const devenv = {
    baseUrl: 'https://localhost:7212'
};

const dockerenv = {
    baseUrl: 'movie.api'
};

let env = devenv;
if (process.env.ENV === 'development') {
    env = dockerenv;
}

export const environment = env;

