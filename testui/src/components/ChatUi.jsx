import React from 'react'
import './chatUi.css'
import upArrow from './assets/arrow.svg'
import { useState } from 'react'
import { useRef } from 'react'
import webLogo from './assets/webLogo.svg'
import userLogo from './assets/user.svg'
import axios from 'axios'


function ChatUi() {
  const [prompt, setprompt] = useState('')
  // const [tempPrompt, settempPrompt] = useState('')
  const inputRef = useRef(null)
  const [isClipped, setIsClipped] = useState(false);
  const [response, setresponse] = useState('')
  const [loading, setloading] = useState(false)
  const handleChange = (e) => {
    setprompt(e.target.value)
  }





  const handleContainerClip = () => {
    setIsClipped((prev) => !prev); // Toggle the clipped state
  };
  const formatCode = (input) => {
    // Replace single quote with semicolon
    let formatted = input.replace(/'/g, ';');

    // Indent braces and new lines
    formatted = formatted.split(/({)/g).join('\n');
    formatted = formatted.split(/(})/g).join('\n');



    return formatted;
  };

  const handleClick = () => {
    const formData = new FormData();
    formData.append('prompt', prompt);
    console.log(formData)
    setloading(true)
    axios(
      {
        method: 'post',
        url: 'http://localhost:5000/AI',
        data: formData,
        headers: { 'Content-Type': 'multipart/form-data' },
      }
    ).then((res) => {
      console.log(res.data)
      let resArray = (res.data).split("**");
      let newArray;
      for (let i = 0; i < resArray.length; i++) {
        if (i === 0 || i % 2 === 0) {
          newArray += resArray[i]
        }
        else {
          newArray += "<b style='font-size:20px'>" + resArray[i] + "</b>"
        }
      }
      newArray = newArray.split("```")
      let responseArray;
      for (let i = 0; i < newArray.length; i++) {
        if (i % 2 === 0 || i === 0) {
          responseArray += newArray[i]
        }
        else {
          
          // let code = formatCode(resArray[i])
          responseArray += "</br><div style='background-color: rgb(90,90,90); padding: 10px; border-radius: 5px; margin-bottom: 10px;'><code>" + formatCode(newArray[i]) + "</code></div></br>"

        }
      }
      let newArray2 = responseArray.split("*").join("</br>")
      newArray2 = newArray2.split("`").join("</br>")
      setresponse(newArray2.split("undefined")[2])
      setloading(false)
    })
    inputRef.current.value = ''
  }

  return (
    <>
      <div className={`chatContainer ${isClipped ? 'clip' : ''}`} >
        <div className="uperContent">
          <div className="userPrompt">
            {
              prompt ? <div className="info" style={{ position: 'absolute', left: '-39px' }}>
                <i><img src={userLogo} alt="" srcset="" /></i>
                <p>user</p>
              </div> : <div></div>
            }
            <p style={{ wordWrap: 'break-word' }}>{prompt}</p>
          </div>
          <div className="response">
            {
              response ? <div className="info" style={{ position: 'absolute', left: '230px' }}>
                <i><img src={webLogo} style={{ width: '30px', height: '30px', marginLeft: '32.5px' }} /></i>
                <p>Assitant</p>
              </div> : <div></div>
            }
            {loading ? <div className="loader">
              <hr />
              <hr />
              <hr />
            </div> : <p style={{ wordWrap: 'break-word' }} dangerouslySetInnerHTML={{ __html: response }}></p>}
          </div>
          <button className="logo" onClick={handleContainerClip}>
                    <img src={webLogo}/>
                </button>
        </div>
        <div className="inputSection">
          <textarea  cols="30" rows="10" onChange={handleChange} ref={inputRef} placeholder="enter your query here" className='inpBox'></textarea>
          <button className='upArrow' onClick={handleClick}>
            <i><img src={upArrow} /></i>
          </button>
        </div>
      </div>
    </>
  )
}

export default ChatUi