FROM node:alpine
 
WORKDIR /usr/app
COPY ./src/Movie.Api/ClientApp /usr/app

RUN npm install 
#
RUN npm run build
# 
EXPOSE 80
# Start the app
CMD [ "npm", "run", "preview" ]