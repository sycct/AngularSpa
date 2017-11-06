import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app.module';

platformBrowserDynamic().bootstrapModule(AppModule)
    .then((success: any) => console.log('App Bootstrapped'))
    .catch((err: any) => console.error(err));