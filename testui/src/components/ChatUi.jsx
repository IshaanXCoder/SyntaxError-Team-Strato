import React from 'react'
import './chatUi.css'
import upArrow from './assets/arrow.svg'
import { useState } from 'react'
import { useRef } from 'react'
import webLogo from './assets/webLogo.svg'

function ChatUi() {
    const [prompt, setprompt] = useState('')
    const [tempPrompt, settempPrompt] = useState('')
    const inputRef = useRef(null)
    const [isClipped, setIsClipped] = useState(false);

    const handleChange = (e) => {
        settempPrompt(e.target.value)
    }
    const handleClick =  () => {
        setprompt(tempPrompt)
        inputRef.current.value = ''
    }

    const handleContainerClip = () => {
        setIsClipped((prev) => !prev); // Toggle the clipped state
    };

    return (
        <>
          <div className={`chatContainer ${isClipped ? 'clip' : ''}`} >
          <div className="uperContent">
                <div className="userPrompt" style={{left:prompt ? '20px':'-800px',transition:'.5s all ease'}}>
                    <p style={{wordWrap:'break-word'}}>{prompt}</p>
                </div>
                <button className="logo" onClick={handleContainerClip}>
                    <img src={webLogo}/>
                </button>
            </div>
            <div className="inputSection">
            <textarea name="" id="" cols="30" rows="10" onChange={handleChange} ref={inputRef} placeholder="enter your query here" className='inpBox'></textarea>
            <button className='upArrow' onClick={handleClick}>
                <i><img src={upArrow}  /></i>
            </button>
            </div>
          </div>
        </>
    )
}

export default ChatUi