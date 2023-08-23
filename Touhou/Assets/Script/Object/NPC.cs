// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class NPC : MonoBehaviour, Interactable
// {
//     public TextMeshProUGUI textMeshPro;

//     private ObjectManager objectManager;

//     private NPCData data;
//     private SpriteRenderer spriteRenderer;
//     private string objPath;
//     public string id;

//     private void Awake()
//     {
//         data = new NPCData();
//         id = this.gameObject.name;
//         objectManager = ObjectManager.Instance;
//     }
//     private void Start()
//     {
//         SetupData();
//         objectManager.AddObjectData(SceneManager.GetActiveScene().name, data);
//         textMeshPro = GameObject.Find("DialogueManager/Canvas/Image")?.GetComponentInChildren<TextMeshProUGUI>();
//     }

//     private void SetupData()
//     {
//         data.name = this.gameObject.name;
//         objPath = "Prefabs/" + data.name;
//         data.obj = Resources.Load<GameObject>(objPath);
//         data.id = id;
//         data.position = this.transform.position;
//     }
//     public void Interact(GameObject scanObj)
//     {
//         textMeshPro.text = "이것의 이름은 " + scanObj.name + "이라고 한다";
//     }
// }
