# Unity资源加载方式详解

## 1. Resources.Load
**优点：**
- 使用简单，直接通过路径加载
- 无需额外设置，开箱即用

**缺点：**
- 所有Resources文件夹中的资源都会被打包到应用中，增加包体大小
- 无法运行时更新资源
- 随着项目增大，加载性能会下降
- 不支持细粒度的内存管理

**示例：**
```csharp
GameObject prefab = Resources.Load<GameObject>("Prefabs/MyPrefab");
GameObject instance = Instantiate(prefab);
```

## 2. AssetBundle
**优点：**
- 可以分包管理资源，减小初始包体大小
- 支持运行时下载和更新
- 可以根据平台加载不同资源
- 支持资源依赖管理

**缺点：**
- 使用复杂，需要编写打包和加载脚本
- 依赖关系管理较困难
- 缓存管理需要自行实现

**示例：**
```csharp
// 加载AssetBundle
AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "mybundle"));

// 从AssetBundle中加载资源
GameObject prefab = bundle.LoadAsset<GameObject>("MyPrefab");
GameObject instance = Instantiate(prefab);

// 使用完后释放
bundle.Unload(false);
```

## 3. Addressable Asset System
**优点：**
- Unity官方推荐的资源管理系统
- 简化了AssetBundle的使用复杂度
- 自动处理依赖关系
- 支持远程加载和缓存
- 内置内存管理和资源生命周期管理
- 支持资源热更新

**缺点：**
- 学习曲线较陡
- 需要额外安装包

**示例：**
```csharp
// 通过地址加载资源
Addressables.LoadAssetAsync<GameObject>("MyPrefab").Completed += (operation) => 
{
    GameObject prefab = operation.Result;
    GameObject instance = Instantiate(prefab);
};

// 释放资源
Addressables.Release(prefabReference);
```

## 4. Direct Reference
**优点：**
- 最简单直接的方式
- 编辑器中可视化管理
- 编译时检查类型安全

**缺点：**
- 所有引用的资源都会被打包
- 无法动态加载或更换资源
- 不适合大量或需要动态加载的资源

**示例：**
```csharp
public class MyScript : MonoBehaviour
{
    public GameObject myPrefab; // 在Inspector中拖拽赋值
    
    void Start()
    {
        GameObject instance = Instantiate(myPrefab);
    }
}
```

## 5. WWW/UnityWebRequest
**优点：**
- 可以从任何URL加载资源
- 支持异步加载
- 可用于加载网络资源或本地文件

**缺点：**
- 需要处理网络错误和超时
- 加载速度受网络影响
- 需要自行管理缓存

**示例：**
```csharp
// 使用UnityWebRequest (推荐，WWW已过时)
IEnumerator LoadAssetCoroutine(string url)
{
    using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url))
    {
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            GameObject prefab = bundle.LoadAsset<GameObject>("MyPrefab");
            GameObject instance = Instantiate(prefab);
        }
    }
}
```

## 6. StreamingAssets
**优点：**
- 文件不会被处理，保持原始格式
- 适合存储视频、音频等大型文件
- 在所有平台都可访问

**缺点：**
- 在移动平台上访问方式不同
- 所有文件都会被打包进应用
- 不能在运行时更新

**示例：**
```csharp
// 在不同平台上访问StreamingAssets
string path = Path.Combine(Application.streamingAssetsPath, "myfile.json");

// 在Android上需要使用WWW或UnityWebRequest
#if UNITY_ANDROID && !UNITY_EDITOR
    UnityWebRequest www = UnityWebRequest.Get(path);
    yield return www.SendWebRequest();
    string json = www.downloadHandler.text;
#else
    string json = File.ReadAllText(path);
#endif
```

## 7. Scriptable Objects
**优点：**
- 可以创建数据资源
- 支持在编辑器中编辑
- 可以通过Resources或AssetBundle加载

**缺点：**
- 主要用于数据存储，不适合加载预制体等复杂资源
- 需要创建自定义类

**示例：**
```csharp
// 定义ScriptableObject
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MyData", order = 1)]
public class MyData : ScriptableObject
{
    public string myString;
    public int myInt;
}

// 加载ScriptableObject
MyData data = Resources.Load<MyData>("Data/MyData");
```

## 8. Asset Database (仅编辑器模式)
**优点：**
- 可以访问项目中的任何资源
- 不需要特殊文件夹结构

**缺点：**
- 仅在编辑器模式下可用，不能在构建后的应用中使用
- 主要用于编辑器工具开发

**示例：**
```csharp
#if UNITY_EDITOR
using UnityEditor;

GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/MyPrefab.prefab");
GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
#endif
```

## 选择合适的资源加载方式

对于现代Unity项目，推荐的资源加载方式是：

- **小型项目**：Resources.Load或Direct Reference足够
- **中型项目**：考虑使用Addressable Asset System
- **大型项目**：Addressable Asset System或自定义AssetBundle系统
- **需要热更新**：Addressable Asset System或AssetBundle

Unity官方正在逐步推广Addressable Asset System作为Resources和AssetBundle的替代方案，它结合了两者的优点并解决了许多缺点。