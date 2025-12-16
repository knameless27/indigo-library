# Etapa 1: build
FROM node:20-alpine AS builder

# Crear carpeta de trabajo
WORKDIR /app

# Copiar package.json y package-lock.json o pnpm-lock.yaml si tienes
COPY package*.json ./

# Instalar dependencias
RUN npm install

# Copiar todo el proyecto
COPY . .

# Construir la app
RUN npm run build

# Etapa 2: producción
FROM node:20-alpine

WORKDIR /app

# Instalar un server ligero para servir los assets estáticos
RUN npm install -g serve

# Copiar build de la etapa anterior
COPY --from=builder /app/dist ./dist

# Puerto que usará el contenedor
EXPOSE 4173

# Comando por defecto para servir la app
CMD ["serve", "-s", "dist", "-l", "4173"]
