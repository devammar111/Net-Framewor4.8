// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  name: 'LOCAL',
  production: false,
  dataApiUrl: "http://localhost:4233/api/",
  filePickerApi: {
    key: 'AqCmIA40Rta5UyDRy2dbMz',
    policy: 'eyJleHBpcnkiOjQxMDIzNDA0MDAsImNhbGwiOlsicmVhZCIsInN0b3JlIiwiY29udmVydCIsInJlbW92ZSJdfQ==',
    signature: '674a4c936d7e19b36ed6f1b032133706165ff693c167748c027879fb94b5bfdd',
    path: '/pbase-dev/',
    location: 'azure'
  },
  userIdle: { idle: 1800, timeout: 5, ping: 0 }
};
/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
