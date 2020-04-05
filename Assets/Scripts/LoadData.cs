using SimpleJSON;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class LoadData : MonoBehaviour
{
    public Text title;
    public GameObject header;
    public GameObject content;
    public GameObject headerTextPrefab;
    public GameObject rowTextPrefab;
    public GameObject rowPrefab;

    // Start is called before the first frame updates
    void Start()
    {

        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Refresh();

        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void GetData()
    {

        // Load and Parse JSON 
        string path = Application.streamingAssetsPath + "/JsonChallenge.json";
        string JSONString = File.ReadAllText(path);
        var json = JSON.Parse(JSONString);
  
       
        //Set Title 
        title.GetComponent<Text>().text = json["Title"];


        //Set Headers
        foreach (var item in json["ColumnHeaders"])
        {
            GameObject ht = Instantiate(headerTextPrefab, header.transform);
            ht.transform.SetParent(header.transform);
            ht.GetComponent<Text>().text = item.Value;

        }

        //Generate Rows
        for (int i = 0; i < json["Data"].Count; i++)
        {

            GameObject row = Instantiate(rowPrefab, content.transform);
            row.transform.SetParent(content.transform);


            // Setting values in columns 
            for (int j = 0; j < json["ColumnHeaders"].Count; j++)
            {

                GameObject rt = Instantiate(rowTextPrefab, row.transform);
                rt.transform.SetParent(row.transform);
                rt.GetComponent<Text>().text = json["Data"][i][j]? json["Data"][i][j].Value : "";
            }

        }


    }

    // received one object and clear his childrens 
    void Clear(GameObject go)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            Destroy(go.transform.GetChild(i).gameObject);
        }
    }

    // Load data again 
    public void Refresh()
    {
        Debug.Log("update");
        Clear(header);
        Clear(content);
        GetData();
    }

    public void ClosedApp()
    {
        Application.Quit();
    }
}
