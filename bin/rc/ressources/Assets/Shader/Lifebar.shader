Shader "3I/WaveShader" { 
	Properties{ 
		_Width ("Width", float) = 1 
		_Height ("Height", float) = 1 
		_Hps ("Hps", int) = 10 
		_MaxHps ("MaxHps", int) = 10 
		_Ratio ("Ratio", float) = 1.7777 
		} 
		
	SubShader { 
		Pass { 
			CGPROGRAM 
			#pragma vertex vert 
			#pragma fragment frag 
			#include "UnityCG.cginc" 
			float _Width; 
			float _Height; 
			float _Hps; 
			float _MaxHps; 
			float _Ratio; 
			struct v2f { 
				float4 pos : SV_POSITION; float3 color : COLOR0; 
				}; 
			v2f vert (appdata_base v) { 
				v2f o; float4 projecPivot = mul (UNITY_MATRIX_MVP, float4(0, 0, 0, 1)); 
				o.pos = projecPivot + float4(v.vertex.x / _Ratio * _Width * (_Hps / _MaxHps), v.vertex.y * _Height, v.vertex.z, 0); 
				o.color = float3(1-(_Hps / _MaxHps),(_Hps / _MaxHps),0); 
				//o.color = float3(0, 0, 0);
				return o; } 
			half4 frag (v2f i) : COLOR { 
				return half4 (i.color, 1); 
				} 
			ENDCG 
		} 
	} 
	Fallback "VertexLit" 
}