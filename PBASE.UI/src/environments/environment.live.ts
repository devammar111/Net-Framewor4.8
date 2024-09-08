export const environment = {
  name: 'LIVE',
  production: true,
  dataApiUrl: "https://pbase-api-live.azurewebsites.net/api/",
  filePickerApi: {
    key: 'AqCmIA40Rta5UyDRy2dbMz',
    policy: 'eyJleHBpcnkiOjQxMDIzNDA0MDAsImNhbGwiOlsicmVhZCIsInN0b3JlIiwiY29udmVydCIsInJlbW92ZSJdfQ==',
    signature: '674a4c936d7e19b36ed6f1b032133706165ff693c167748c027879fb94b5bfdd',
    path: '/pbase-live/',
    location: 'azure'
  },
  userIdle: { idle: 1800, timeout: 5, ping: 0 }
};
