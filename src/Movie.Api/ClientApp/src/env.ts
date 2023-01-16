let env = {
    baseUrl: 'https://localhost:7212'
};
if (process && process.env) {
    env = {
        baseUrl: process.env.PROXY_API || 'https://localhost:7212'
    };
}

export const environment = env;

