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
    EnumField rarityField;
    EnumField statTypeField;
    EnumField increaseTypeField;
    IntegerField minLevelField;
    Label statusLabel;


    FloatField minAttackField;
    FloatField maxAttackField;
    FloatField minCoolDownField;
    FloatField maxCoolDownField;
    FloatField maxSpeedField;
    FloatField minSpeedField;
    FloatField maxRadiusField;
    FloatField minRadiusField;
    ObjectField dependancyField;
    [MenuItem("Tools/Upgrade Creator")]
    static void Open()
    {
        var window = GetWindow<UpgradeEditor>();
        window.titleContent = new GUIContent("Upgrade Creator");
        window.minSize = new Vector2(320, 220);
    }
    VisualElement bonusGroup;

    void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = root.style.paddingRight = 8;
        root.style.paddingTop = 8;
        bonusGroup = new VisualElement();

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

        minLevelField = new IntegerField("Minimum level requirement");

        rarityField = new EnumField("Rarity", Rarity.COMMON) ;

        
        minAttackField = new FloatField("New Weapon Min Strength");
        maxAttackField = new FloatField("New Weapon Max Strength");
        minCoolDownField = new FloatField("New Weapon Min Cooldown");
        maxCoolDownField= new FloatField("New Weapon Max Cooldown");
        minSpeedField = new FloatField("New Weapon Min Speed");
        maxSpeedField = new FloatField("New Weapon Max Speed");
        minRadiusField = new FloatField("New Weapon Min Radius");
        maxRadiusField = new FloatField("New Weapon Max Radius");
        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
        root.Add(rarityField);
        root.Add(upgradeTypeField);
        root.Add(minLevelField);

        root.Add(bonusGroup);
       
     

        root.Add(dependancyField);
        root.Add(createButton);
        root.Add(statusLabel);
        OnTypeChanged();
        upgradeTypeField.RegisterValueChangedCallback(type => OnTypeChanged());

    }
    void OnTypeChanged()
    {
        bonusGroup.Clear();
        switch ((UpgradeType)upgradeTypeField.value)
        {
      
            case UpgradeType.reduceCoolDownRanged:
            case UpgradeType.reduceCoolDownMelee:
            case UpgradeType.meleeWeaponSizeUp:
            case UpgradeType.heal:
       
                bonusGroup.Add(increaseTypeField);
                bonusGroup.Add(increaseAmountField);
                break;
            case UpgradeType.statChange:
                bonusGroup.Add(increaseTypeField);
                bonusGroup.Add(increaseAmountField);
                bonusGroup.Add(statTypeField);
                break;
            case UpgradeType.newMeleeWeapon:
                bonusGroup.Add(minAttackField);
                bonusGroup.Add(maxAttackField);
                bonusGroup.Add(minCoolDownField);
                bonusGroup.Add(maxCoolDownField);
                break;
            case UpgradeType.newRangedWeapon:
                bonusGroup.Add(minAttackField);
                bonusGroup.Add(maxAttackField);

                bonusGroup.Add(minCoolDownField);
                bonusGroup.Add(maxCoolDownField);

              
                bonusGroup.Add(minSpeedField);
                bonusGroup.Add(maxSpeedField);

                break;
            case UpgradeType.newShieldWeapon:
                bonusGroup.Add(minAttackField);
                bonusGroup.Add(maxAttackField);
                
                bonusGroup.Add(minRadiusField);
                bonusGroup.Add(maxRadiusField);

              
                bonusGroup.Add(minSpeedField);
                bonusGroup.Add(maxSpeedField);
                break;
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
        upgrade.upgradeLevel = (Rarity)rarityField.value;
        upgrade.minLevel = minLevelField.value;
        
        upgrade.minWeaponAttack = minAttackField.value;
        upgrade.maxWeaponAttack= maxAttackField.value;
       
        upgrade.minWeaponSpeed=minSpeedField.value;
        upgrade.maxWeaponSpeed=maxSpeedField.value;
        
        upgrade.maxWeaponCooldown = maxCoolDownField.value;
        upgrade.minWeaponCooldown= minCoolDownField.value;
        
        upgrade.minWeaponRadius= minRadiusField.value;
        upgrade.maxWeaponRadius= maxRadiusField.value;

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