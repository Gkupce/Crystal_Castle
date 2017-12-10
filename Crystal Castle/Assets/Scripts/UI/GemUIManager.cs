using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemUIManager : MonoBehaviour {

    public Sprite[] crystalSprites;

    public Image[] UICrystalImages;
    public Text[] UICrystalAmountLabels;
	public Text[] UIButtonHints;

    public static GemUIManager Instance;

	private const string MAX = "MAX";


    private void Awake()
    {
		if (Instance != null && Instance != this)
			Destroy (this);
		else
			Instance = this;
    }


    public void AddGem(int index, GemManager.GemType type, int amount)
    {
        UICrystalImages [index].enabled = type != GemManager.GemType.None;
		UIButtonHints [index].enabled = type == GemManager.GemType.None;

        UICrystalImages [index].sprite = crystalSprites [(int)type];
		UICrystalAmountLabels [index].fontSize = (amount == GemManager.MAX_GEMS ? 20 : 30);
		UICrystalAmountLabels [index].text = (amount == 0 ? "" : amount == GemManager.MAX_GEMS ? MAX : amount.ToString());
    }
}
