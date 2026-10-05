using Codice.Client.BaseCommands;
using Codice.Client.Common.FsNodeReaders;
using log4net.Filter;
using System.Drawing.Printing;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public class UpgradeEditor : EditorWindow
{
    const string ResourcesPath = "Assets/Resources/Upgrades";

    TextField nameField;
    FloatField increaseAmountField;
    EnumField upgradeTypeField;

    EnumField statTypeField;
    EnumField increaseTypeField;

    Label statusLabel;


    FloatField minAttackField;
    FloatField maxAttackField;
    FloatField minCoolDownField;
    FloatField maxCoolDownField;
    ObjectField dependancyField;
    [MenuItem("FutureGames/Upgrade Creator")]
    static void Open()
    {
        var window = GetWindow<UpgradeEditor>();
        window.titleContent = new GUIContent("Upgrade Creator");
        window.minSize = new Vector2(320, 220);
    }
    

    void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = root.style.paddingRight = 8;
        root.style.paddingTop = 8;

        root.Add(new Label("Create a new upgrade asset")
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 6 }
        });

        nameField = new TextField("Item Name");
        upgradeTypeField = new EnumField("Upgrade Type",UpgradeType.statChange);
        increaseAmountField = new FloatField("Increase Amount");
        increaseTypeField=new EnumField("Increase Type", IncreaseType.ADDITIVE);
        statTypeField = new EnumField("Stat Type", StatType.Strength);
        dependancyField = new ObjectField("Require other upgrade?");
        dependancyField.objectType= typeof(SO_UpgradeData);


        minAttackField = new FloatField("New Weapon Min Strenth");
        maxAttackField = new FloatField("New Weapon Max Strenth");
        minCoolDownField = new FloatField("New Weapon Min Cooldown");
        maxCoolDownField= new FloatField("New Weapon Max Cooldown");
        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
        root.Add(upgradeTypeField);
        root.Add(statTypeField);
       
        root.Add(increaseAmountField);
        root.Add(increaseTypeField);

        root.Add(dependancyField);
        root.Add(createButton);
        root.Add(statusLabel);
        root.Add(minAttackField);
        root.Add(maxAttackField);
        root.Add(minCoolDownField);
        root.Add(maxCoolDownField);
      
    }
    
    private void OnGUI()
    {
        statTypeField.visible = (UpgradeType)upgradeTypeField.value == UpgradeType.statChange;

        if ((UpgradeType)upgradeTypeField.value == UpgradeType.newRangedWeapon ||
            (UpgradeType)upgradeTypeField.value == UpgradeType.newMeleeWeapon ||
            (UpgradeType)upgradeTypeField.value == UpgradeType.newShieldWeapon)
        {
            minAttackField.visible = true;
            maxAttackField.visible = true;
            minCoolDownField.visible = true;
            maxCoolDownField.visible = true;
        }
        else
        {
            minAttackField.visible =   false;
            maxAttackField.visible =   false;
            minCoolDownField.visible = false;
            maxCoolDownField.visible = false;
        }
           

    }

    void CreateAsset()
    {
        
        
        
        if (string.IsNullOrWhiteSpace(nameField.value))
        {
            statusLabel.text = "Item name is required.";
            return;
        }

        Directory.CreateDirectory(ResourcesPath);

        var upgrade = ScriptableObject.CreateInstance<SO_UpgradeData>();
        upgrade.upgradeText= nameField.value;
        upgrade.upgradeType = (UpgradeType)upgradeTypeField.value;
        upgrade.statType = (StatType)statTypeField.value;
        upgrade.increaseType = (IncreaseType)increaseTypeField.value;
        upgrade.increaseAmount = increaseAmountField.value;
        upgrade.dependancy = (SO_UpgradeData)dependancyField.value;

        string safeName = string.Join("_", nameField.value.Split(Path.GetInvalidFileNameChars()));
        string path = AssetDatabase.GenerateUniqueAssetPath($"{ResourcesPath}/{safeName}.asset");

        AssetDatabase.CreateAsset(upgrade, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorGUIUtility.PingObject(upgrade);
        Selection.activeObject = upgrade;
        statusLabel.text = $"Created {path}";
    }
}