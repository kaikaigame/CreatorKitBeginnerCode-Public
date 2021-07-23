﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace CreatorKitCode 
{
    /// <summary>
    /// Handles displaying VFX in the game. One instance should be in the scene, and is part of the Manager prefab.
    /// It will create pools of the VFX prefabs defined in the given VFXDatabase, and through the GetVFX/PlayVFX
    /// methods allows you to get an instance at a given position. The pools are rotating queue, so when you are
    /// finished with an instance, that instance will be moved to the back of the available instance queue.
    /// </summary>
    public class VFXManager : MonoBehaviour
    {
        /// <summary>
        /// One instance of VFX.
        /// The Effect field is the actual GameObject of the effect you can disable once it is finished.
        /// The Index field is used by the manager to know which effect this instance is part of.
        /// </summary>
        public class VFXInstance
        {
            public GameObject Effect;
            public int Index;
        }
    
        static VFXManager Instance { get; set; }
    
        public VFXDatabase Database;

        Queue<VFXInstance>[] m_Instances;
    
        void Awake()
        {
            Instance = this;
            Init();
        }

        void Init()
        {
            m_Instances = new Queue<VFXInstance>[Database.Entries.Length];
        
            for (int i = 0; i < Database.Entries.Length; ++i)
            {
                m_Instances[i] = new Queue<VFXInstance>();
                CreateNewInstances(i);
            }
        }

        void CreateNewInstances(int index)
        {
            var entry = Database.Entries[index];
        
            for (int j = 0; j < entry.PoolSize; ++j)
            {
                VFXInstance vfxInstance = new VFXInstance();
                var inst = Instantiate(entry.Prefab);
                inst.gameObject.SetActive(false);

                vfxInstance.Effect = inst;
                vfxInstance.Index = index;
                
                m_Instances[index].Enqueue(vfxInstance);
            }
        }

        /// <summary>
        /// Return a VFXInstance of the given type. use instance.Effect to access the gameobject.
        /// It will be SetActive by the function, so you just have to place it.
        /// </summary>
        /// <param name="type">The type of VFX wanted. This enum will be generated by a tool in the Editor based on the VFXDatabase entries</param>
        /// <returns>The instance that was in front of the pool queue</returns>
        public static VFXInstance GetVFX(VFXType type)
        {
            int idx = (int)type;
            if (Instance.m_Instances[idx].Count == 0)
            {
                Instance.CreateNewInstances(idx);
            }

            //we put the instance from the from to the back of the queue. Perfect for one shot particle, it will have been
            //disabled before being back on front of the list
            var inst = Instance.m_Instances[idx].Dequeue();
            Instance.m_Instances[idx].Enqueue(inst);
        
            inst.Effect.gameObject.SetActive(true);
            return inst;
        }

        /// <summary>
        /// This is a shortcut, will get a VFX of the given type and will place it at the given position
        /// </summary>
        /// <param name="type">The type of VFX wanted. This enum will be generated by a tool in the Editor based on the VFXDatabase entries</param>
        /// <param name="position">Where the VFX will be placed</param>
        /// <returns>The Instance it just placed, can be useful if the effect isn't auto-disabled (e.g. looping effect)</returns>
        public static VFXInstance PlayVFX(VFXType type, Vector3 position)
        {
            var i = GetVFX(type);
            i.Effect.gameObject.transform.position = position;

            //使用后特效消失
            if (type == VFXType.FireEffect)
            {
                Destroy(i.Effect.gameObject, 1f);
            }

            return i;
        }
    }
}