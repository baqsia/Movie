FROM node:alpine

RUN mkdir /app
WORKDIR /app

COPY ./src/Movie.Api/ClientApp/package-lock.json /app
COPY ./src/Movie.Api/ClientApp/package.json /app

COPY ./src/Movie.Api/ClientApp /app

RUN npm install

EXPOSE 5173
 
CMD ["npm", "run", "dev"]
