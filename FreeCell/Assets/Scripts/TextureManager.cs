using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextureManager : MonoBehaviour
{
    private static Dictionary<Symbols, Sprite> _symbolSpriteDic;

    public static Dictionary<Symbols, Sprite> SymbolSpriteDic => _symbolSpriteDic;

    public IEnumerator CoDownload(TextureVersions textureVersion, ushort productVersion)
    {
        _symbolSpriteDic = new Dictionary<Symbols, Sprite>();

        string fileName, readPath, writePath;

        for (int i = 0; i < 4; i++)
        {
            fileName = $"{i}.png";
            readPath = string.Format("{0}/{1}/{2}/{3}", Application.streamingAssetsPath, textureVersion.ToString(), $"Version_{productVersion}", fileName);
            writePath = string.Format("{0}/{1}", Application.persistentDataPath, fileName);

            if (File.Exists(writePath) == false)
            {
                using (UnityWebRequest reqeust = UnityWebRequestTexture.GetTexture(readPath))
                {
                    yield return reqeust.SendWebRequest();

                    File.WriteAllBytes(writePath, reqeust.downloadHandler.data);
                }
            }

            var tex = new Texture2D(2, 2);
            tex.LoadImage(File.ReadAllBytes(writePath));
            var sp = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one * 0.5f, 100.0f);

            _symbolSpriteDic.Add((Symbols)i, sp);
        }
    }
}
