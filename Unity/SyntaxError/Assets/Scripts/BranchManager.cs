using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;

using Scripts.Nodes;
using Scripts.Gates;
using Scripts.Branches;
using Scripts.Nodes.Binary;
using Scripts.Gates.Prefabs;

namespace Scripts
{
    public sealed class BranchManager : SingletonPattern<BranchManager>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private string gateTag;
        
        [SerializeField] private List<InputNode> inputs;
        [SerializeField] private List<OutputNode> outputs;
        
        [SerializeField] private List<Node> inNodes;
        [SerializeField] private List<Node> outNodes;

        [SerializeField] private GameObject branchPrefab;
        [SerializeField] private Transform branchParent;
        private List<Branch> branches;

        void Start()
        {
            branches = new List<Branch>();
        }
        
        public void CreatePrefab(string name)
        {
            var data = new PrefabData(name, ForceResult(1, 1), ForceResult(0, 1), ForceResult(1, 0), ForceResult(0, 0));

            DeleteAll();
            CircuitManager.Instance.CreateCustom(data);
            
            inputs[0].SetState(0);
            inputs[1].SetState(0);
            
            outputs[0].SetState(0);
        }

        private int ForceResult(int in1, int in2)
        {
            inputs[0].SetState(in1 == 0 ? 1 : 0);
            inputs[1].SetState(in2 == 0 ? 1 : 0);
            
            inputs[0].Toggle();
            inputs[1].Toggle();

            return outputs[0].State;
        }
        
        public void RegisterNewBranch(OutNode start)
        {
            var instance = Instantiate(branchPrefab, branchParent).GetComponent<Branch>();
            start.SetBranch(instance);
            instance.SetNodeIn(start);
            
            branches.Add(instance);

            ToggleNodes();
        }
        
        public void CompleteBranch(InNode end)
        {
            var instance = branches[branches.Count - 1];
            if (end.BranchConnected)
                DeleteBranch(instance);
            else
            {
                end.SetBranch(instance);
                instance.SetNodeOut(end);
            }
            
            ToggleNodes();
        }

        public void DeleteBranch(Branch branch)
        {
            branch.ClearConnections();
            branches.Remove(branch);
            Destroy(branch.gameObject);
        }
        
        public void PushGate(Gate gate)
        {
            foreach (var node in gate.NodesIn)
                inNodes.Add(node);
            
            foreach (var node in gate.NodesOut)
                outNodes.Add(node);
        }
        
        public void DeleteGate(Gate gate)
        {
            for(int i = 0; i < inNodes.Count; i++)
                if (gate.NodesIn.Contains(inNodes[i]))
                {
                    inNodes.RemoveAt(i);
                    i--;
                }
            
            for(int i = 0; i < outNodes.Count; i++)
                if (gate.NodesOut.Contains(outNodes[i]))
                {
                    outNodes.RemoveAt(i);
                    i--;
                }
            
            Destroy(gate.gameObject);
            CircuitManager.Instance.ToggleDeletePrompt(false);
        }

        private void DeleteAll()
        {
            foreach (var branch in branches)
            {
                branch.ClearConnections();
                Destroy(branch.gameObject);
            }
            
            branches.Clear();
            
            foreach (var node in GameObject.FindGameObjectsWithTag(gateTag))
                Destroy(node.gameObject);
            
            inNodes.Clear();
            outNodes.Clear();
        }

        private void ToggleNodes()
        {
            ToggleNodes(inNodes);
            ToggleNodes(outNodes);
            ToggleNodes(inputs);
            ToggleNodes(outputs);
        }
        
        private void ToggleNodes<T>(List<T> nodes) where T : Node
        {
            foreach (var node in nodes)
                node.ToggleClickable();
        }
    }
}