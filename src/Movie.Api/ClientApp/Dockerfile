#FROM node:alpine
## Create app directory
#WORKDIR /app
#
#COPY ./src/Movie.Api/ClientApp/package*.json /app
#RUN npm install
#
#COPY  ./src/Movie.Api/ClientApp /app
#
#EXPOSE 3000
#CMD ["npm", "run", "dev"]

FROM node:alpine
 
EXPOSE 5173
 
COPY ./src/Movie.Api/ClientApp/package*.json package.json 
RUN npm install
 
COPY . .
 
CMD ["npm", "run", "dev"]