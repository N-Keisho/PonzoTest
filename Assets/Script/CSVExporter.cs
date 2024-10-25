using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class CSVExporter : MonoBehaviour {
    private string dirPath;
    private string filePath;
    private string dirName = "Results";
    private string extension = ".csv";
    private string fileName = "";

    private void Start() {
        // 実行環境に応じてCSVファイルのパスを設定 
        #if  UNITY_EDITOR
        dirPath = Path.Combine(Application.dataPath, dirName); 
        #else 
        dirPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), dirName); 
        #endif 

        fileName = PlayerPrefs.GetString("ID", "Nan") + extension;
        filePath = Path.Combine(dirPath, fileName);
    }

    // CSVファイルにデータを書き込むメソッド
    public void WriteToFile(string data) {
        // ディレクトリが存在しない場合、新しいディレクトリを作成
        if (!Directory.Exists(dirPath)) {
            Directory.CreateDirectory(dirPath);
        }
        // ファイルが存在しない場合、新しいファイルを作成
        if (!File.Exists(filePath)) {
            File.WriteAllText(filePath, data + "\n", Encoding.UTF8);
        }
        // ファイルが存在する場合、データの重複をチェック
        else {
            string existingData = File.ReadAllText(filePath, Encoding.UTF8);
            if (!existingData.Contains(data)) {
                File.AppendAllText(filePath, data + "\n", Encoding.UTF8);
            }
        }
    }
}