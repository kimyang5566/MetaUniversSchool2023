using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicControl : MonoBehaviour
{
    public float testTime;
    const float fpsMeasurePeriod = 0.5f;//固定时间为0.5s
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    public int m_CurrentFps;
    public int nowGraphicLevel;
    const string display = "{0} FPS";//输出格式
    
    private void Update()
    {
        // measure average frames per second
        //计算每秒的平均帧数
        m_FpsAccumulator++;
        if (Time.realtimeSinceStartup > m_FpsNextPeriod)
        {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
            m_FpsAccumulator = 0;
            m_FpsNextPeriod += fpsMeasurePeriod;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Time.realtimeSinceStartup  开始的真实时间
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;//下一次刷新时间
        InvokeRepeating("GraphicTesting",0.1f,testTime);
    }

    void GraphicTesting()
    {
        int tmpLevel = 0;
        if (m_CurrentFps < 30)
        {
            if ((nowGraphicLevel - 1) >= 0)
            {
                tmpLevel = QualitySettings.GetQualityLevel() - 1;
            }
        }
        else
        {
            tmpLevel = 5;
        }
        //if (nowGraphicLevel != tmpLevel)
        //{
        QualitySettings.SetQualityLevel(nowGraphicLevel);
           
        //}
        nowGraphicLevel = tmpLevel;
    }
}
