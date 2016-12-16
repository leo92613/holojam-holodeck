Shader "Custom/TwoSidedFabric" {
//	Properties {
//		_Color ("Color", Color) = (1,1,1,1)
//		_MainTex ("Albedo (RGB)", 2D) = "white" {}
//		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0
//	}
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		LOD 200
//		
//		CGPROGRAM
//		// Physically based Standard lighting model, and enable shadows on all light types
//		#pragma surface surf Standard fullforwardshadows
//
//		// Use shader model 3.0 target, to get nicer looking lighting
//		#pragma target 3.0
//
//		sampler2D _MainTex;
//
//		struct Input {
//			float2 uv_MainTex;
//		};
//
//		half _Glossiness;
//		half _Metallic;
//		fixed4 _Color;
//
//		void surf (Input IN, inout SurfaceOutputStandard o) {
//			// Albedo comes from a texture tinted by color
//			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//			o.Albedo = c.rgb;
//			// Metallic and smoothness come from slider variables
//			o.Metallic = _Metallic;
//			o.Smoothness = _Glossiness;
//			o.Alpha = c.a;
//		}
//		ENDCG
//	}
//	FallBack "Diffuse"
//}
//
//Shader "DoubleSided" {
     Properties {
         _Color ("Main Color", Color) = (1,1,1,1)
         _MainTex ("Base (RGB)", 2D) = "white" {}
         //_BumpMap ("Bump (RGB) Illumin (A)", 2D) = "bump" {}
     }
     SubShader {     
         //UsePass "Self-Illumin/VertexLit/BASE"
         //UsePass "Bumped Diffuse/PPL"
         // Ambient pass
        Pass {
         Name "BASE"
         Tags {"LightMode" = "Always" /* Upgrade NOTE: changed from PixelOrNone to Always */}
         Color [_PPLAmbient]
         SetTexture [_BumpMap] {
             constantColor (.5,.5,.5)
             combine constant lerp (texture) previous
             }
         SetTexture [_MainTex] {
             constantColor [_Color]
             Combine texture * previous DOUBLE, texture*constant
             }
         }
     // Vertex lights
     Pass {
         Name "BASE"
         Tags {"LightMode" = "Vertex"}
         Material {
             Diffuse [_Color]
             Emission [_PPLAmbient]
             Shininess [_Shininess]
             Specular [_SpecColor]
             }
         SeparateSpecular On
         Lighting On
         Cull Off
         SetTexture [_BumpMap] {
             constantColor (.5,.5,.5)
             combine constant lerp (texture) previous
             }
        SetTexture [_MainTex] {
 
             Combine texture * previous DOUBLE, texture*primary
             }
         }
     } 
     FallBack "Diffuse", 1
 }