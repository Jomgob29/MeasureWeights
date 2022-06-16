using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Profiling.Memory;
using UnityEngine.UI;
using System.IO;


public class memorysize : MonoBehaviour
{
   private string webInfo;
   public string url;
   string[] url_names;


    public Material[] materials;
    public Texture2D[] textures;
    public List<string> newEntryName;
    public string[] filternames;
    public float bytes;
    

    public Text memoryUI;
    public Text memoryUI_v2;
    public string overall_sizes;
    public int textureCount;
    public int totalTextureMemorySize = 0;
    HideFlags hideFlagMask = HideFlags.HideInInspector | HideFlags.HideAndDontSave;
    HideFlags hideFlagMask1 = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy | HideFlags.NotEditable | HideFlags.DontUnloadUnusedAsset;



    private void Start()
    {
        textures = Resources.FindObjectsOfTypeAll<Texture2D>();
        memoryUI.text = "Total Allocated Memory: " + (Profiler.GetTotalAllocatedMemoryLong() / 1000000f).ToString("#,###,###.####") + "MB" + "\n" + 
        "Total Unused Reserved Memory: " +(Profiler.GetTotalUnusedReservedMemoryLong() / 1000000f).ToString("#,###,###.####") + "MB"; 
    }



    void ReadMemorySizes()
    {
        foreach (Texture2D t in textures)
        {

            bytes += Profiler.GetRuntimeMemorySize(t);

            foreach (string filtername in filternames)
            {
                    if (t.name.Equals(filtername))
                    {
                        memoryUI.text += "NAME:  " + t.name + "   |   " + " RUNTIME MEMORY SIZE " +
                        (Profiler.GetRuntimeMemorySizeLong(t) / 1000f).ToString("#,###,###.####") + "KB      " + " / " +
                        Profiler.GetRuntimeMemorySizeLong(t) / 1000000f + "MB" + "\n";
                   }
            }
        }


        /*
        for(int i = 0; i < url_names.Length; i++)
        {
            string search = url_names[i];

            for(int j = 0; j < textures.Length; j++)
            {
               if(textures[j].name.Contains("Texture") || textures[j].name.Contains("texture"))
                {
                    Debug.Log(textures[j].name);
                }
                
            }
        }
        */
    }

   

    public void UpdateProfile(InputField input)
    {
        foreach (Texture2D t in textures)
        {
             if (t.name.Contains(input.text))
             {
                 memoryUI_v2.text += "NAME:  " + t.name + "   |   " + " USED MEMORY " +
                 (Profiler.GetRuntimeMemorySize(t) / 1000f).ToString("#,###,###.####") + "KB" + " / " +
                 Profiler.GetRuntimeMemorySize(t) / 1000000f + "MB" + "\n";
             }
        }

        memoryUI.text = "Total Allocated Memory: " + (Profiler.GetTotalAllocatedMemory() / 1000000f) + " MB";
    }




    IEnumerator GetText()
    {
        WWW w = new WWW(url);
        yield return w;

        webInfo = w.text;
        url_names = webInfo.Split('\n');
      //  ReadMemorySizes();
    }





}



