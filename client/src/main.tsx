import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import {DevTools} from "jotai-devtools";
import App from './App.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
      <DevTools/>
    <App />
  </StrictMode>,
)
