float4x4 World;
float4x4 View;
float4x4 Projection;

float4x4 WorldInverseTranspose;
float3 LigthDir = float3(1, 0, 0);
float4 DiffColor = float4(1, 1, 1, 1); //white
float Intensity = 1.0;

float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.1;

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Normal : NORMAL0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

VertexShaderOutput DiffuseVertexShaderFunc(VertexShaderInput input)
{
	VertexShaderOutput output;

		float4 worldPosition = mul(input.Position, World);
		float4 viewPosition = mul(worldPosition, View);
		output.Position = mul(viewPosition, Projection);

		float4 normal = mul(input.Normal, WorldInverseTranspose);
			float ligthIntens = dot(normal, LigthDir);
		output.Color = saturate(DiffColor * Intensity * ligthIntens);

		return output;
		 
}
float DiffusePixelShaderFunc(VertexShaderOutput input) : COLOR0
{
	return saturate(input.Color + AmbientColor * AmbientIntensity);
}

technique Diffuse
{
	pass Pass1
	{
		VertexShader = compile vs_4_0 DiffuseVertexShaderFunc();
		PixelShader = compile ps_4_0 DiffusePixelShaderFunc();
	}
}
