Shader "Custom/shader_surface"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)

		_FrontColor ("Front Color", Color) = (1,1,1,1)
		_BackColor ("Back Color", Color) = (1,1,1,1)

        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

		_NormalTex ("Normal Map", 2D) = "white" {}
		_NormalDetailTex ("Normal Details", 2D) = "white" {}

		_Transparency("Transparency", Range(0,1)) = 1.0

		_WaveSpeedA("Wave Speed A", Range(-2,2)) = 1.0
		_WaveSpeedB("Wave Speed B", Range(-2,2)) = 1.0

		_WavePower("Wave Power", Range(0,1)) = 1.0

		_Fresnel("Fresnel Power", Range(0,8)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha


        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _NormalTex;
		sampler2D _NormalDetailTex;
		fixed _Transparency;
		fixed _WaveSpeedA;
		fixed _WaveSpeedB;
		fixed _WavePower;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_NormalTex;
			float2 uv_NormalDetailTex;

			float3 viewDir;
			float3 worldNormal;

			INTERNAL_DATA
        };

		// float half fixed
        half _Glossiness;
        half _Metallic;
		fixed _Fresnel;
        fixed4 _Color;

		fixed4 _FrontColor;
		fixed4 _BackColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

		/*
		fixed3 Albedo;  // diffuse color
    fixed3 Normal;  // tangent space normal, if written
    fixed3 Emission;
    half Specular;  // specular power in 0..1 range
    fixed Gloss;    // specular intensity
    fixed Alpha;    // alpha for transparencies
		*/


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed3 Normal = UnpackNormal(tex2D (_NormalTex, IN.uv_NormalTex + float2(_WaveSpeedA * _Time.y, 0) ));

			fixed3 NormalDetail = UnpackNormal(tex2D (_NormalDetailTex, IN.uv_NormalDetailTex + float2(_WaveSpeedB * _Time.y, 0)));

			o.Normal = lerp(o.Normal, normalize(Normal + NormalDetail), _WavePower);

			float Rim = 1 - saturate( dot( o.Normal, IN.viewDir ));

			Rim = pow(Rim, _Fresnel);

            o.Albedo = lerp(_BackColor, _FrontColor, Rim);         
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Transparency;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
