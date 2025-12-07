Shader "Custom/MaglaRata"
{
Properties
    {
        _PointA ("Point A", Vector) = (0, 0, 0, 0) // Position for Point A
        _Radius ("Radius", Float) = 5.0 // Radius of the circles
        _EdgeWidth ("Edge Width", Float) = 0.1 // Edge width of the circles
        _Transparency ("Circle Transparency", Range(0, 1)) = 1.0 // Default fully opaque
        

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            Tags { "Queue"="Overlay" } // Ensure it's rendered above opaque objects
            Blend SrcAlpha OneMinusSrcAlpha // Enable transparency blending

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Properties
            float4 _PointA;
            float _PointsNum = 512;
            float4 _Points[512];
            float _Radius;
            float _EdgeWidth;
            float _Transparency;

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float3 worldPos : TEXCOORD0; // World position of the vertex
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; // Get world position
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {

                
                // Get distance from Point A and Point B
                float distA = length(i.worldPos - _PointA.xyz);

                // Calculate transparency for Circle A and Circle B based on distance
                float edgeA = smoothstep(_Radius - _EdgeWidth, _Radius + _EdgeWidth, distA);

                // Calculate the alpha value for both circles based on proximity to the points
                float alphaA = lerp(0.0, _Transparency, edgeA); // Transparency for Circle A
               


                // Combine the results of both circles (both are drawn, but with different transparencies)
                float alpha = min(alphaA, alphaA); 

                float3 iwp = i.worldPos;
                


                for(int x = 0; x < _PointsNum; x++)
                {

                    float distX = length(iwp - _Points[x].xyz);
                    float edgeX = smoothstep(_Radius - _EdgeWidth, _Radius + _EdgeWidth, distX);
                    float alphaX = lerp(0.0, _Transparency, edgeX);

                    alpha = min(alpha, alphaX);
                }


                // Return the color (black) with transparency based on the max of both circles
                return half4(0, 0, 0, alpha);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
