import packageInfo from '../../package.json';

export const environment = {
  appVersion: packageInfo.version,
  production: true,
  apiEndpoint:"https://localhost:7105/api"
};
