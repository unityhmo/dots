Shader "Layered/ThreeTexturesMasked" {
	Properties {
		_rTex ("Red Channel", 2D) = "red" {}
		_gTex ("Green Channel", 2D) = "green" {}
        _bTex ("Blue Channel", 2D) = "blue" {}
		_Mask ("Mask (RGB)", 2D) = "grey" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		// Mask three layers with RGB image, WITH SHADOWS
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		
		sampler2D _rTex;
		sampler2D _gTex;
		sampler2D _bTex;
		sampler2D _Mask;
		
		struct Input
		{
			float2 uv_rTex;
			float2 uv_gTex;
			float2 uv_bTex;
			float2 uv_Mask;
		};
		
		half _Glossiness;
		half _Metallic;
		
		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 tr = tex2D(_rTex, IN.uv_rTex);
			fixed4 tg = tex2D(_gTex, IN.uv_gTex);
			fixed4 tb = tex2D(_bTex, IN.uv_bTex);
			fixed4 mask = tex2D(_Mask, IN.uv_Mask);

			fixed4 rgba = (tr * mask.r) + (tg * mask.g) + (tb * mask.b);

			o.Albedo = rgba.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}
		
		ENDCG
	}
	
	FallBack "Diffuse"
}