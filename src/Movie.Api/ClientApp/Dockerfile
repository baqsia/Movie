# ==== CONFIGURE =====
# Use a Node 16 base image
FROM node:14.17.0 
WORKDIR . 
COPY . .

RUN npm i

EXPOSE 5137

RUN npm run dev
