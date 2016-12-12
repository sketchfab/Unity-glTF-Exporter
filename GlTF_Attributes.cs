﻿using UnityEngine;
using System.Collections;

public class GlTF_Attributes : GlTF_Writer {
	public GlTF_Accessor normalAccessor;
	public GlTF_Accessor positionAccessor;
	public GlTF_Accessor colorAccessor;
	public GlTF_Accessor texCoord0Accessor;
	public GlTF_Accessor texCoord1Accessor;
	public GlTF_Accessor texCoord2Accessor;
	public GlTF_Accessor texCoord3Accessor;
	public GlTF_Accessor lightmapTexCoordAccessor;
	public GlTF_Accessor jointAccessor;
	public GlTF_Accessor weightAccessor;
	public GlTF_Accessor tangentAccessor;

	private Vector4[] boneWeightToBoneVec4(BoneWeight[] bw)
	{
		Vector4[] bones = new Vector4[bw.Length];
		for (int i=0; i < bw.Length; ++i)
		{
			bones[i] = new Vector4(bw[i].boneIndex0, bw[i].boneIndex1, bw[i].boneIndex2, bw[i].boneIndex3);
		}

		return bones;
	}

	private Vector4[] boneWeightToWeightVec4(BoneWeight[] bw)
	{
		Vector4[] weights = new Vector4[bw.Length];
		for (int i = 0; i < bw.Length; ++i)
		{
			weights[i] = new Vector4(bw[i].weight0, bw[i].weight1, bw[i].weight2, bw[i].weight3);
		}

		return weights;
	}

	public void Populate (Mesh m)
	{
		positionAccessor.Populate (m.vertices);
		if(colorAccessor != null)
		{
			colorAccessor.Populate(m.colors);
		}
		if (normalAccessor != null)
		{
			normalAccessor.Populate (m.normals);
		}
		if (texCoord0Accessor != null)
		{
			texCoord0Accessor.Populate (m.uv, false);
		}
		if (texCoord1Accessor != null)
		{
			texCoord1Accessor.Populate (m.uv2, false);
		}
		if (texCoord2Accessor != null)
		{
			texCoord2Accessor.Populate (m.uv3, false);
		}
		if (texCoord3Accessor != null)
		{
			texCoord3Accessor.Populate (m.uv4, false);
		}
		if(lightmapTexCoordAccessor != null)
		{
			lightmapTexCoordAccessor.PopulateWithOffsetScale(m.uv2, false);
		}
		if(jointAccessor != null)
		{
			Vector4[] bones = boneWeightToBoneVec4(m.boneWeights);
			jointAccessor.Populate(bones);
		}
		if(weightAccessor != null)
		{
			Vector4[] weights = boneWeightToWeightVec4(m.boneWeights);
			weightAccessor.Populate(weights);
		}
		if(tangentAccessor != null)
		{
			tangentAccessor.Populate(m.tangents, false);
		}
	}

	public override void Write ()
	{
		Indent();	jsonWriter.Write ("\"attributes\": {\n");
		IndentIn();
		if (positionAccessor != null)
		{
			CommaNL();
			Indent();	jsonWriter.Write ("\"POSITION\": \"" + positionAccessor.id + "\"");
		}
		if (normalAccessor != null)
		{
			CommaNL();
			Indent();	jsonWriter.Write ("\"NORMAL\": \"" + normalAccessor.id + "\"");
		}
		if (colorAccessor != null)
		{
			CommaNL();
			Indent(); jsonWriter.Write("\"COLOR\": \"" + colorAccessor.id + "\"");
		}
		if (texCoord0Accessor != null)
		{
			CommaNL();
			Indent();	jsonWriter.Write ("\"TEXCOORD_0\": \"" + texCoord0Accessor.id + "\"");
		}
		if (texCoord1Accessor != null)
		{
			CommaNL();
			Indent();	jsonWriter.Write ("\"TEXCOORD_1\": \"" + texCoord1Accessor.id + "\"");
		}
		if (texCoord2Accessor != null)
		{
			CommaNL();
			Indent();	jsonWriter.Write ("\"TEXCOORD_2\": \"" + texCoord2Accessor.id + "\"");
		}
		if (texCoord3Accessor != null)
		{
			CommaNL();
			Indent();	jsonWriter.Write ("\"TEXCOORD_3\": \"" + texCoord3Accessor.id + "\"");
		}
		if (lightmapTexCoordAccessor != null)
		{
			CommaNL();
			Indent(); jsonWriter.Write("\"TEXCOORD_4\": \"" + lightmapTexCoordAccessor.id + "\"");
		}
		if (jointAccessor != null)
		{
			CommaNL();
			Indent(); jsonWriter.Write("\"JOINT\": \"" + jointAccessor.id + "\"");
		}
		if (weightAccessor != null)
		{
			CommaNL();
			Indent(); jsonWriter.Write("\"WEIGHT\": \"" + weightAccessor.id + "\"");
		}
		if (tangentAccessor != null)
		{
			CommaNL();
			Indent(); jsonWriter.Write("\"TANGENT\": \"" + tangentAccessor.id + "\"");
		}
		//CommaNL();
		jsonWriter.WriteLine();
		IndentOut();
		Indent();	jsonWriter.Write ("}");
	}

}
