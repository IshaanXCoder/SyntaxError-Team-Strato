import { useState } from 'react'
import './App.css'
import axios from 'axios'
import ChatUi from './components/chatUI'

function App() {
  const [count, setCount] = useState(0)
  // const [prompt, setprompt] = useState('')
  // const [response, setresponse] = useState('')
  
  return (
    <>
    <ChatUi/>
    
    </>
  )
}

export default App
