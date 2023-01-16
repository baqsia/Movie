import {defineConfig, loadEnv} from 'vite'
import react from '@vitejs/plugin-react'
const env = loadEnv(
    'mock',
    process.cwd(),
    ''
)
const processEnvValues = {
  'process.env': Object.entries(env).reduce(
      (prev, [key, val]) => {
        return {
          ...prev,
          [key]: val,
        }
      },
      {},
  )
}
export default defineConfig({
  plugins: [react()],
  build: {
    rollupOptions: {
      external: [
        /^node:.*/,
      ]
    }
  },
  define: processEnvValues
})
