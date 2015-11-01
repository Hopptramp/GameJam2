Shader "Outlined/Diffuse" 
{
	Properties 
	{
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" { }
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.00, 0.03)) = .005
    }
    SubShader 
    {
        Pass 
        {

			Blend SrcAlpha OneMinusSrcAlpha
			
		    CGPROGRAM
		    #pragma vertex vert
		    #pragma fragment frag

		    #include "UnityCG.cginc"
		    
		     

		    fixed4 _Color;
		    sampler2D _MainTex;

		    struct v2f 
		    {
		        float4 pos : SV_POSITION;
		        float2 uv : TEXCOORD0;
		    };

		    float4 _MainTex_ST;

		    v2f vert (appdata_base v)
		    {
		        v2f o;
		        o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
		        o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
		        return o;
		    }

		    fixed4 frag (v2f i) : SV_Target
		    {
		        fixed4 texcol = tex2D (_MainTex, i.uv);
		        return texcol * _Color;
		    }
		    ENDCG

        }
        
        Pass
        {
        	Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			
	        CGPROGRAM
	        #pragma vertex vert
	        #pragma fragment frag

	        #include "UnityCG.cginc"
	        
	        float _Outline;
			float4 _OutlineColor;
			 
			struct v2f 
			{
				float4 pos : POSITION;
				float4 colour : COLOR;
			};
			
			v2f vert(appdata_base v) 
			{
				// just make a copy of incoming vertex data but scaled according to normal direction
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			 
				float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float2 offset = TransformViewToProjection(norm.xy);
			 
				o.pos.xy += offset * o.pos.z * _Outline;
				o.colour = _OutlineColor;
				return o;
			}
			
			float4 frag(v2f i) :COLOR 
			{ 
				return i.colour; 
			}
			
			ENDCG
        }
    }
}
	