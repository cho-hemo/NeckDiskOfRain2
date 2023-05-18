Shader "Custom/BlurShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Blur("Blur", Float) = 10
    }
    SubShader
    {

        Tags{ "Queue" = "Transparent" }

        GrabPass
        {   
        }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 vertColor : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                o.vertColor = v.color;
                return o;
            }

            sampler2D _GrabTexture;
            fixed4 _GrabTexture_TexelSize;

            float _Blur;

            half4 frag(v2f i) : SV_Target
            {
                float blur = _Blur;
                blur = max(1, blur);

                fixed4 col = (0, 0, 0, 0);
                float weight_total = 0;

                [loop]
                for (float x = -blur; x <= blur; x += 1)
                {
                    float distance_normalized = abs(x / blur);
                    float weight = exp(-0.5 * pow(distance_normalized, 2) * 5.0);
                    weight_total += weight;
                    col += tex2Dproj(_GrabTexture, i.grabPos + float4(x * _GrabTexture_TexelSize.x, 0, 0, 0)) * weight;
                }

                col /= weight_total;
                return col;
            }
            ENDCG
        }
        GrabPass
        {   
        }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 vertColor : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                o.vertColor = v.color;
                return o;
            }

            sampler2D _GrabTexture;
            fixed4 _GrabTexture_TexelSize;

            float _Blur;

            half4 frag(v2f i) : SV_Target
            {
                float blur = _Blur;
                blur = max(1, blur);

                fixed4 col = (0, 0, 0, 0);
                float weight_total = 0;

                [loop]
                for (float y = -blur; y <= blur; y += 1)
                {
                    float distance_normalized = abs(y / blur);
                    float weight = exp(-0.5 * pow(distance_normalized, 2) * 5.0);
                    weight_total += weight;
                    col += tex2Dproj(_GrabTexture, i.grabPos + float4(0, y * _GrabTexture_TexelSize.y, 0, 0)) * weight;
                }

                col /= weight_total;
                return col;
            }
            ENDCG
        }

    }
}

// Shader "Custom/ImageEffect/TextureBasedBlurring"
// {
//     Properties
//     {
//         _MainTex ("Texture", 2D) = "white" {}
//         _BlurTex ("BlurMap", 2D) = "black" {}
//         _Sample  ("Sample", Range(4, 64)) = 16
//         _Effect  ("Effect", float) = 1
//         _Threshold ("Threshold", float) = 0.001
//     }
//     SubShader
//     {
//         Tags { "RenderType"="Opaque" }
//         LOD 100

//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
            
//             #include "UnityCG.cginc"

//             struct appdata
//             {
//                 float4 vertex : POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             struct v2f
//             {
//                 float2 uv : TEXCOORD0;
//                 UNITY_FOG_COORDS(1)
//                 float4 vertex : SV_POSITION;
//             };

//             sampler2D _MainTex;
//             sampler2D _BlurTex;
            
//             float4 _MainTex_ST;
//             float4 _BlurTex_ST;
//             float _Sample;
//             float _Effect;
//             float _Threshold;
            
//             v2f vert (appdata v)
//             {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//                 UNITY_TRANSFER_FOG(o,o.vertex);
//                 return o;
//             }

//             fixed4 frag (v2f i) : SV_Target
//             {
//                 // sample the texture
//                 fixed4 col = fixed4(0,0,0,0);
//                 //  R = X, G = Y => RG = UV offset
//                 float2 mDir = float2(0,0);
//                 float mIntensity = 0;
//                 float divider = 1/_Sample;
                
//                 float4 blurSum = float4(0, 0, 0, 1);
//                 blurSum.xy = mDir * divider;
               
//                 for(int j = 0; j < _Sample; j++)
//                 {
//                     float scale = j * divider;
//                     float2 cCoord = i.uv + (.5-i.uv) * blurSum.xy * _Effect * scale;
//                     float4 offset = tex2D(_BlurTex, cCoord);
                  
//                     mDir = offset.rg - .4961;
//                     if(length(mDir) <= _Threshold)
//                         mDir = 0;
//                     else
//                         mDir += .4961;
//                     blurSum.xy += mDir * divider;
//                     mIntensity = offset.b;
//                     mIntensity = pow(mIntensity, 3);
//                     col += tex2D(_MainTex, cCoord) * divider;
//                 }
//                 return col;
//             }
//             ENDCG
//         }
//     }
// }