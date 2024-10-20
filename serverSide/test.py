import google.generativeai as genai
import json
from flask import Flask,request,jsonify
import flask_cors
import nltk
from nltk.tokenize import word_tokenize
from nltk.tag import pos_tag
from nltk.chunk import ne_chunk
import os 
app = Flask(__name__)  
flask_cors.CORS(app) 
genai.configure(api_key=os.environ["API_KEY"])
model = genai.GenerativeModel("gemini-1.5-flash")
chat = model.start_chat(
    history=[
        {"role": "user", "parts": "Hello"},
        {"role": "model", "parts": "Great to meet you. What would you like to know?"},
    ]
)


nltk.download('punkt_tab')
nltk.download('averaged_perceptron_tagger_eng')

def serialize(gates):
    for i,gate in enumerate(gates):
        gate["id"] = i+1
    return gates

def getConnections(gates):
    connections = [{}]
    for gate in gates:
        if gate["id"] == len(gates):
            pass
        else:
          (connections[0])[f'{gate["id"]} links to'] = gate["id"]+1
    return connections

def translate_circuit(sentence):
    tokens = word_tokenize(sentence)
    pos_tags = pos_tag(tokens)

    # Example:
    # print(tokens)
    # print(pos_tags)

    gates = []
    connections = []

    # Logic for identifying gates, connections, and inputs
    # ... (This part will require more sophisticated NLP techniques)

    # Example - Simplified for demonstration
    gate_names = ["AND", "OR", "NOT", "NAND", "NOR", "XOR", "XNOR"]
    inputDict = {"AND": [0, 1], "OR": [0, 1], "NOT": [1], "NAND": [0, 1], "NOR": [0, 1], "XOR": [0, 1], "XNOR": [0, 1]}
    for i, token in enumerate(tokens):
        if token in gate_names:
            gate_type = token
            input_values = inputDict[gate_type]
            # ... (Code to determine input values based on context)
            gates.append({"type": gate_type, "input": input_values, "id": i + 1})
    gates = serialize(gates)    
    connections = getConnections(gates)
    return {"gates": gates, "connections": connections}






# print(response.text)

@app.route("/AI",methods=['GET','POST']) 
async def AI():
  response = chat.send_message("i have built my own programming langugage named strato")
  # print(response.text)
  response = chat.send_message('''here is the basic syntax of strato:\n
  out[0] = x // in this language we can only output value at array indexes only
  //final output will be just an array 
  //example code:-

  out[0]  = 12
  out[1] = 34
  //output :-

  12
  34

  //func <name> <argument list> => { //body } 
  func add x y => {
    x = x + y
  }

  //in is a vector containing integers thats the input of a gate
  //out is a vector containing integers thats the output of a gate 

  out[0] = add(in[0], 20)

  //output
  30

  //loop <expression> => { //body } 
  loop x > 10 => {
    x = x - 1
  }

  //if <expression> => { //body } 
  if x > 10 => {
    out[0] = x
  }
                              ''')
  if request.method == 'POST':
      prompt = request.form['prompt']
      response =  chat.send_message(prompt)
      return jsonify(response.text)
  return 'hello'


@app.route('/physics',methods=['GET','POST'])
def physics():
  response = chat.send_message("from now on you will be a physics instructor,help me in physics")
  if request.method == 'POST':
      prompt = request.form['prompt']
      response =  chat.send_message(prompt)
      return jsonify(response.text)
  return 'hello'

@app.route('/mathematics', methods=['GET', 'POST'])
def mathematics():
    response = chat.send_message("from now on you will be a mathematics instructor, help me in mathematics")
    if request.method == 'POST':
        prompt = request.form['prompt']
        response = chat.send_message(prompt)
        return jsonify(response.text)
    return 'hello'


@app.route('/computer-science', methods=['GET', 'POST'])
def computer_science():
    response = chat.send_message("from now on you will be a computer science instructor, help me in computer science")
    if request.method == 'POST':
        prompt = request.form['prompt']
        response = chat.send_message(prompt)
        return jsonify(response.text)
    return 'hello'

@app.route('/nlpParser', methods=['GET', 'POST'])
def nlpParser():
    if request.method == 'POST':
        prompt = request.form['prompt']
        response = translate_circuit(prompt)
        return jsonify(response)
    return 'hello'

if __name__ == '__main__':
    app.run(debug=True,port=5000)

'''GenerateContentResponse(
    done=True,
    iterator=None,
    result=protos.GenerateContentResponse({
      "candidates": [
        {
          "content": {
            "parts": [
              {
                "text": "Hi there! \ud83d\udc4b  What can I do for you today? \n"
              }
            ],
            "role": "model"
          },
          "finish_reason": "STOP",
          "index": 0,
          "safety_ratings": [
            {
              "category": "HARM_CATEGORY_SEXUALLY_EXPLICIT",
              "probability": "NEGLIGIBLE"
            },
            {
              "category": "HARM_CATEGORY_HATE_SPEECH",
              "probability": "NEGLIGIBLE"
            },
            {
              "category": "HARM_CATEGORY_HARASSMENT",
              "probability": "NEGLIGIBLE"
            },
            {
              "category": "HARM_CATEGORY_DANGEROUS_CONTENT",
              "probability": "NEGLIGIBLE"
            }
          ]
        }
      ],
      "usage_metadata": {
        "prompt_token_count": 733,
        "candidates_token_count": 13,
        "total_token_count": 746
      }
    }),
)'''