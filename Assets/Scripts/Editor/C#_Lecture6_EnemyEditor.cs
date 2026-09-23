using Codice.Client.Common.FsNodeReaders;
using System.Drawing.Printing;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public class EnemyEditor : EditorWindow
{
    const string ResourcesPath = "Assets/Resources/Enemies";

    TextField nameField;
    TextField descriptionField;
    IntegerField maxHpField;
    Label statusLabel;
    EnumField typeField;

    ObjectField spriteField;

    FloatField shotSpeedField;
    FloatField speedField;
    FloatField distanceField;
    FloatField expField;
    IntegerField startLevelField;
    IntegerField endLevelField;
    [MenuItem("FutureGames/Enemy Creator")]
    static void Open()
    {
        var window = GetWindow<EnemyEditor>();
        window.titleContent = new GUIContent("Enemy Creator");
        window.minSize = new Vector2(320, 220);
    }
    

    void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = root.style.paddingRight = 8;
        root.style.paddingTop = 8;

        root.Add(new Label("Create a new Enemy asset")
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 6 }
        });

        nameField = new TextField("Item Name");
    
        maxHpField = new IntegerField("MaxHp");
        spriteField = new ObjectField("Sprite");
        typeField = new EnumField("Enemy Type", EnemyType.Chasing);
        shotSpeedField = new FloatField("Bullet Speed");
        speedField = new FloatField("Movement Speed");
        distanceField = new FloatField("Distance to keep from player");
        expField = new FloatField("Exp Yield");
        startLevelField = new IntegerField("Level enemy starts spawning at");
        endLevelField = new IntegerField("Level enemy stops spawning at");


        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
       
        root.Add(maxHpField);
        root.Add(spriteField);
        root.Add(expField);
        root.Add(speedField);
        root.Add(typeField);
        root.Add(distanceField);
        root.Add(shotSpeedField);
        root.Add(startLevelField);
        root.Add(endLevelField);
        root.Add(createButton);
        root.Add(statusLabel);
      
    }
    private void Update()
    {
        shotSpeedField.visible = (EnemyType)typeField.value == EnemyType.Shooting;
        distanceField.visible = (EnemyType)typeField.value == EnemyType.Shooting;
    }

    void CreateAsset()
    {
        if (spriteField.value.GetType() != typeof(Sprite)||spriteField.value==null)
        {
            statusLabel.text = "Invalid Sprite Field";
            return;
        }
        if (string.IsNullOrWhiteSpace(nameField.value))
        {
            statusLabel.text = "Item name is required.";
            return;
        }

        Directory.CreateDirectory(ResourcesPath);

        var enemy = ScriptableObject.CreateInstance<SO_Enemy>();
        enemy.EnemyName = nameField.value;
        enemy.enemyType = (EnemyType)typeField.value;
        enemy.MaxHP = maxHpField.value;
        enemy.Sprite = (Sprite)spriteField.value;
        enemy.distanceToKeep= distanceField.value;
        enemy.expYield = expField.value;
        enemy.Speed= speedField.value;
        enemy.shotSpeed= shotSpeedField.value;
        enemy.startLevel= startLevelField.value;
        enemy.endLevel= endLevelField.value;

        string safeName = string.Join("_", nameField.value.Split(Path.GetInvalidFileNameChars()));
        string path = AssetDatabase.GenerateUniqueAssetPath($"{ResourcesPath}/{safeName}.asset");

        AssetDatabase.CreateAsset(enemy, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorGUIUtility.PingObject(enemy);
        Selection.activeObject = enemy;
        statusLabel.text = $"Created {path}";
    }
}