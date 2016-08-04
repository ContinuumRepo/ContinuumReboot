Shader "BendingShaders/Bend_Runner_Unlit" {
   Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _QOffset ("Offset", Vector) = (14,-12,0,0)
	  _Dist ("Distance", Float) = 80.0
	  _Insensitive ("Insensitive", Float) = 0.40
    }
    
    SubShader {
      Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
     Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _QOffset;
			float _Dist;
			float _Insensitive;
			
			struct v2f {
			    float4 pos : SV_POSITION;
			    float4 uv : TEXCOORD0;
			};

			v2f vert (appdata_full v)
			{				
			    v2f o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    
			    float xsmeh=sin(_WorldSpaceCameraPos.z/120);
			 			    
			    float zOff = vPos.z/_Dist;
			    			    
			    _QOffset.x=_QOffset.x*xsmeh;

			    if(zOff<-_Insensitive)
			    {
			    	zOff+=_Insensitive;
			   		vPos += _QOffset*zOff*zOff;
			    }
			    
			    o.pos = mul (UNITY_MATRIX_P, vPos);
			    o.uv = v.texcoord;
			    return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
			    half4 col = tex2D(_MainTex, i.uv.xy);
			    
			    return col;
			}
			ENDCG
		}
	}
}
