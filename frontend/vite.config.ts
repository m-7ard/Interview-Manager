import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vite.dev/config/
export default defineConfig({
    plugins: [react()],
    css: {
        preprocessorOptions: {
            scss: {
                quietDeps: true, // Suppresses dependency warnings
            },
            sass: {
                quietDeps: true, // If you're using indented syntax
            },
        },
    },
})
