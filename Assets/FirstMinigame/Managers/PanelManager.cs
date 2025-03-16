using System;
using System.Linq;
using UnityEngine;
public class PanelManager : MonoBehaviour
{
    [SerializeField] private PanelModel[] panels;

    public void ShowPanrl(string panelId)
    {
        PanelModel panel = panels.FirstOrDefault(p => p.panelId == panelId);
    }
}

