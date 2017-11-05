using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemUIManager : MonoBehaviour {

    public Sprite[] crystalSprites;

    public Image[] UICrystalImages;
    public Text[] UICrystalAmountLabels;
	public Text[] UIButtonHints;

    public static GemUIManager instance;


    private void Awake()
    {
        instance = this;
    }


    public void AddGem(int index, GemManager.GemType type, int amount)
    {
        UICrystalImages [index].enabled = type != GemManager.GemType.None;
		UIButtonHints [index].enabled = type == GemManager.GemType.None;

        UICrystalImages [index].sprite = crystalSprites [(int)type];
        UICrystalAmountLabels [index].text = (amount == 0 ? "" : amount.ToString());
    }
}
