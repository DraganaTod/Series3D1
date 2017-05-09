float4x4 World;
float4x4 View;
float4x4 Projection;

float4x4 WorldInverseTranspose;
float3 LigthDir = float3(1, 0, 0);
float4 DiffColor = float4(1, 1, 1, 1); //white
float Intensity = 1.0;

float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.1;

texture ModelTex;
sampler2D texSamp = sampler_state{
	Texture = (ModelTex);
	MagFilter = Linear;
	MinFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Normal : NORMAL0;
	float2 TexCoord : TEXCOORD0
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float3 Normal : TEXCOORD0;
	float2 TexCoord : TEXCOORD1;
};

VertexShaderOutput TextureVertexShaderFunc(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
		float4 viewPosition = mul(worldPosition, View);
		output.Position = mul(viewPosition, Projection);

	float4 normal = mul(input.Normal, WorldInverseTranspose);
		float ligthIntens = dot(normal, LigthDir);
	output.Color = saturate(DiffColor * Intensity * ligthIntens);
	
	output.Normal = normal;
	output.TexCoord = input.TexCoord;

	return output;

}
float TexturePixelShaderFunc(VertexShaderOutput input) : COLOR0
{
	float4 textureColor = tex2D(texSamp, input.TexCoord);
	textureColor.a = 1;

	return saturate(textureColor * (input.Color) + AmbientColor * AmbientIntensity);
}

technique Texture
{
	pass Pass1
	{
		VertexShader = compile vs_4_0 TextureVertexShaderFunc();
		PixelShader = compile ps_4_0 TexturePixelShaderFunc();
	}
}
