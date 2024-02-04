using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Actions_Stuff.Interfaces;
using UnityEngine;

namespace Actions_Stuff
{
    public class ActionRoutine
    {
        public string name { get; private set; }
        public int id { get; private set; }
        
        public IEnumerator enumerator { get; private set; }
        public Coroutine coroutine { get; private set; }


        public Action startEvent;
        public Action endEvent;
        

        public HashSet<int> blockers;
        public HashSet<int> queuers;
        public HashSet<int> cancellables;
        public HashSet<int> interruptables;
        
        public ActionRoutine(){}

        public void SetName(string name) => this.name = name;
        public void SetID(int id) => this.id = id;
        public void SetEnumerator(IEnumerator enumerator) => this.enumerator = enumerator;

        public static List<int> GetAllCoincidences(IEnumerable<int> a, List<int> b) => a.Where(b.Contains).ToList();

        public void AddBlocker(int id) => blockers.Add(id);
        public void AddQueuer(int id) => queuers.Add(id);
        public void AddCancellable(int id) => cancellables.Add(id);
        public void AddInterruptable(int id) => interruptables.Add(id);
    }
}