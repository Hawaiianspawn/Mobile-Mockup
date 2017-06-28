using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowEmissive : MonoBehaviour {

    public int maxColor;
    public int minColor;
    public float colorChangeSpeed = 1;

    protected Renderer m_Renderer;
    private Material m_Material;
    protected Color m_rainbow;

    void Start()
    {
        if (minColor >= maxColor)
        {
            minColor = 0;
            maxColor = 255;
        }

        StartCoroutine(UpdateRainbow());

        m_Renderer = GetComponent<Renderer>();
        //m_Renderer.effectDistance = new Vector2(2, -2);
        m_Material = m_Renderer.material;
    }

    void Update()
    {
        m_Material.SetColor("_EmissionColor", m_rainbow);
    }

    IEnumerator UpdateRainbow()
    {
        float pMin = (float)minColor / 255;
        float pMax = (float)maxColor / 255;

        m_rainbow = new Color(pMax, pMin, pMin);
        yield return null;

        while (true)
        {
            while (m_rainbow.b < pMax)
            {
                m_rainbow.b += Time.deltaTime * colorChangeSpeed;
                yield return null;
            }
            while (m_rainbow.r > pMin)
            {
                m_rainbow.r -= Time.deltaTime * colorChangeSpeed;
                yield return null;
            }
            while (m_rainbow.g < pMax)
            {
                m_rainbow.g += Time.deltaTime * colorChangeSpeed;
                yield return null;
            }
            while (m_rainbow.b > pMin)
            {
                m_rainbow.b -= Time.deltaTime * colorChangeSpeed;
                yield return null;
            }
            while (m_rainbow.r < pMax)
            {
                m_rainbow.r += Time.deltaTime * colorChangeSpeed;
                yield return null;
            }
            while (m_rainbow.g > pMin)
            {
                m_rainbow.g -= Time.deltaTime * colorChangeSpeed;
                yield return null;
            }
        }
    }
}
