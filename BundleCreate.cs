using UnityEngine;
using UnityEditor;

/*
 * 2020-03-16
 * 创建AssetBundle_(记得将本文件放入Editor目录)
 * Written By Yeliheng
 */
public class BundleCreate
{

    //拓展编辑菜单
    [MenuItem("AssetBundleManager/CreateAssetBundles")]

    static void CreateAsset()
    {

        //存放打包好文件的路径
        string dir = Application.dataPath + "/StreamingAssets";

        //将资源中命名的所有资源打包处理（参数：路径，依附关系，Win平台）
		//查阅手册
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

    }

}
