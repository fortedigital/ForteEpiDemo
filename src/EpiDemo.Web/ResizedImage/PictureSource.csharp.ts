//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

import { ScaleMode } from './ScaleMode.csharp';
import { AspectRatio } from './AspectRatio.csharp';

export interface PictureSource
{
	mediaCondition: string;
	allowedWidths: number[];
	sizes: string[];
	mode: ScaleMode;
	targetAspectRatio: AspectRatio;
	quality?: number;
}
