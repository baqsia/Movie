const devenv = {
    baseUrl: 'http://127.0.0.1:7212'
};

const dockerenv = {
    baseUrl: 'movie.api'
};

let env = devenv;
if (process.env.ENV === 'development') {
    env = dockerenv;
}

export const environment = env;

