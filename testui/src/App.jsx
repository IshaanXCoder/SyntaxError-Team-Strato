import { useState } from 'react'
import './App.css'
import axios from 'axios'
import ChatUi from './components/chatUI'

function App() {
  // const [count, setCount] = useState(0)
  // const [prompt, setprompt] = useState('')
  // const [response, setresponse] = useState('')
  // const handleOnchange = (e) => {
  //   console.log(e.target.value)
  //     setprompt(e.target.value)
  // }
  // const handleClick = () => {
  //   const formData = new FormData();
  //   formData.append('prompt', prompt);
  //   console.log(formData)
  //   axios(
  //     {
  //       method: 'post',
  //       url: 'http://localhost:5000/AI',
  //       data: formData,
  //       headers: { 'Content-Type': 'multipart/form-data' },
  //     }
  //   ).then((res) => {
  //     console.log(res.data)
  //     setresponse(res.data)
  //   })
  // }
  return (
    <>
    <ChatUi/>
    </>
  )
}

export default App
