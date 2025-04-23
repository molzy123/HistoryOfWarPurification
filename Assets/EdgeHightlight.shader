Shader "Custom/TopBottomEdgeOutline" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        // 这个参数控制描边的粗细，值越大，线条越粗
        // 它是基于UV空间的，所以可能需要比预期更大的值 (例如 0.05, 0.1)
        _OutlineWidth ("Outline Width (UV space)", Range(0.001, 0.2)) = 0.05 // 默认值可以改大点试试
        _NormalThreshold ("Normal Threshold", Range(0.9, 1.0)) = 0.99
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _Color;
            float4 _OutlineColor;
            float _OutlineWidth; // 这个值决定了粗细
            float _NormalThreshold;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                bool isTopOrBottom = abs(i.worldNormal.y) > _NormalThreshold;

                // 计算到UV边缘的最小距离
                float distToEdge = min(min(i.uv.x, 1.0 - i.uv.x), min(i.uv.y, 1.0 - i.uv.y));

                // 如果在顶/底面，并且距离UV边缘小于指定的宽度，则视为描边
                bool isNearEdge = distToEdge < _OutlineWidth;

                if (isTopOrBottom && isNearEdge) {
                    // 使用硬边直接显示描边色
                    return _OutlineColor;

                    // --- 或者，如果你想要柔和一点的过渡 ---
                    // float outlineFactor = smoothstep(_OutlineWidth, _OutlineWidth * 0.8, distToEdge);
                    // return lerp(_OutlineColor, col, outlineFactor);
                    // -----------------------------------------
                } else {
                    return col;
                }
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
