using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using AspNet.Security.OpenIdConnect.Server;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Extensions;
using SpaDatasource.Interfaces;
using System.Security.Claims;

namespace AngularSpa.Security
{
    public class AuthorizationProvider : OpenIdConnectServerProvider
    {
        public override Task ValidateTokenRequest(ValidateTokenRequestContext context)
        {
            // Отвергаем запросы на аутенфикацию, которы не соотвествуют grant_type=password или grant_type=refresh_token.
            if (!context.Request.IsPasswordGrantType() && !context.Request.IsRefreshTokenGrantType())
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.UnsupportedGrantType,
                    description: "Only resource owner password credentials and refresh token " +
                                 "are accepted by this authorization server");

                return Task.CompletedTask;
            }

            // Так как мы реализуем аутенфикацию для нашего SPA 
            // это значит что у нас будет только один клиент (Angular приложение)
            // Этот клиент не должен хранить клинтский секретный ключ, так ка кон легко будет доступен 
            // для несанкционированого просмотра.
            // Поэтому в таких случаях сервер должен принимать запрос на аутинфекация без секретного ключа клиента
            // Просто пропускаем этот запрос дальше, если grant_type=password или grant_type=refresh_token
            context.Skip();

            return Task.CompletedTask;
        }

        public override async Task HandleTokenRequest(HandleTokenRequestContext context)
        {
            // Извлекаем из списка сервисов наш менеджер пользователей
            IUserManager userManager = context.HttpContext.RequestServices.GetService(typeof(IUserManager)) as IUserManager; 
            if(userManager == null)
                throw new Exception("User Manager object not configured.");

            // Будем обрабатывать только запросы типа: grant_type=password
            // Запросы типа grant_type=refresh_token будут автоматически обрабатываться ASOS
            if (context.Request.IsPasswordGrantType())
            {
                // Ищем пользователя в БД
                SpaDatasource.Entitites.User user = await userManager.FindByNameAsync(context.Request.Username);
                if (user == null)
                {
                    // Нет пользователя, вернем ошибку
                    context.Reject(
                        error: OpenIdConnectConstants.Errors.InvalidGrant,
                        description: "Invalid credentials.");
                    return;
                }

                // Проверим что пароль задан правильно
                if (!await userManager.CheckPasswordAsync(user, context.Request.Password))
                {
                    // Пароль неверный - вернем ошибку
                    context.Reject(
                        error: OpenIdConnectConstants.Errors.InvalidGrant,
                        description: "Invalid credentials.");

                    return;
                }

                var identity = new ClaimsIdentity(context.Scheme.Name,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Role);

                // subject claimвсегад включается как в Calim Identity так и в 
                // access tokens, даже если это не указано явно.
                // subject - это уникальный идентификатор пользователя
                Claim c = new Claim(OpenIdConnectConstants.Claims.Subject, user.Id.ToString());
                identity.AddClaim(c);

                // Создаем новый билет аутенфикации
                var ticket = new AuthenticationTicket(
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties(),
                    context.Scheme.Name);

                // Устанавлием области (scopes)
                // Это обьязательный минимум
                ticket.SetScopes(
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIdConnectConstants.Scopes.Profile);

                // Возвращаем билет аутенфикации, на основе которого будут сгенерированы два токена
                // access token и refresh token
                context.Validate(ticket);       
            }

            return;
        }

    }
}
