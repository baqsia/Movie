# ==== CONFIGURE =====
# Use a Node 16 base image
FROM node:12
WORKDIR . 
COPY . .

RUN npm i

EXPOSE 5173

RUN npm run dev
