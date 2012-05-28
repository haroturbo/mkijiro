/* 
 * Copyright (C) 2006 aeolusc <soarchin@gmail.com>
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 2 of
 * the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
 */
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <pspkernel.h>
#include <psptypes.h>
#include <pspdisplay.h>
#include <pspsysmem_kernel.h>
#include <pspgu.h>
#include "font.h"
#include "lang_zh_euc.h"
#include "conf.h"
#include "blend.h"
#include "rgb_color.h"
#include "allocmem.h"


void (*font_line)(int x1, int y1, int x2, int y2);
int (*font_outputn)(int sx, int sy, const char *msg, int count);
void (*font_fillrect)(int x1, int y1, int x2, int y2);

t_bgctx bgctx __attribute__(   (  aligned( 4 ), section( ".bss" )  )   );

int font_output(int x, int y, const char *msg)
{
	return font_outputn(x, y, msg, 0x7FFF0000);
}

#ifdef ENGLISH_UI
#define BUFFER_HZCOUNT	(50)
#else
#define BUFFER_HZCOUNT	(450)
#endif
#define BUFFER_HZDATALEN (BUFFER_HZCOUNT*(sizeof(hzfont)))

static u32 buffer_hzset = 0;
static hzfont hzbuffer[BUFFER_HZCOUNT] __attribute__(   (  aligned( 4 ), section( ".bss" )  )   );

static u8 * _get_hzfont(u16 offset)
{
	int i;
	
	for(i=0;i<BUFFER_HZCOUNT;i++){
		if(hzbuffer[i].offset==offset) return hzbuffer[i].dat;
	}
	
		u8 *hzinfo;
		int fd = sceIoOpen(FONT_DIR, PSP_O_RDONLY, 0777);
		if(fd < 0)		return hzbuffer[0].dat;
		
		hzbuffer[buffer_hzset].offset = offset;
		sceIoLseek(fd,2048+offset*18,0);
		sceIoRead(fd,hzbuffer[buffer_hzset].dat,18);
		sceIoClose(fd);
		hzinfo = hzbuffer[buffer_hzset].dat;
		
		buffer_hzset++;
		if(buffer_hzset>=BUFFER_HZCOUNT) buffer_hzset=BUFFER_HZCOUNT/10;		//恷枠序秘産贋議1/10忖範葎械喘忖,音厚仟
		
		return hzinfo;
}

//EUC-JP判定
//0x8F〜0x95まで拡張EUC(JIS213では0x8Fのみ)
int IsHzcode(int x, const char *msg)
{
	return (((((((unsigned char)msg[x] +0x5F)&0xFF)<0x5E)) && ((unsigned char)msg[x + 1] > 0xA0))
		|| ((((((unsigned char)msg[x] +0x71)&0xFF)<0x6)) && ((unsigned char)msg[x + 1] > 0xA0) && ((unsigned char)msg[x + 2] > 0xA0)));
}

static u8 * GetHz(int x, const char *msg)
{
			//http://charset.uic.jp/show/cp51932/
			#define NEXTCODE 94
			#define CODESKIP 8836
			if((((((unsigned char)msg[x] +0x5F)&0xFF)<0x5E)) && ((unsigned char)msg[x + 1] > 0xA0))
				return _get_hzfont( (int)((unsigned char)msg[x] - 0xA1) * NEXTCODE + (int)((unsigned char)msg[x + 1] - 0xA1) );
			//0x8F〜0x95まで拡張EUC(JIS213では0x8Fのみ)
			else if((((((unsigned char)msg[x] +0x71)&0xFF)<0x6)) && ((unsigned char)msg[x + 1] > 0xA0)&& ((unsigned char)msg[x + 2] > 0xA0))
				return _get_hzfont( + (int)((unsigned char)msg[x] - 0x8F +1) * CODESKIP + (int)((unsigned char)msg[x+1] - 0xA1) * NEXTCODE + (int)((unsigned char)msg[x + 2] - 0xA1) );
			else 
				return _get_hzfont(0);//ダミー
}

u8 efont[]__attribute__(   (  aligned( 1 ), section( ".data" )  )   ) =
{
/*21*/ 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
/*22*/ 0x00,0x82,0x08,0x20,0x82,0x08,0x00,0x80,0x00,
/*23*/ 0x01,0x45,0x14,0x00,0x00,0x00,0x00,0x00,0x00,
/*24*/ 0x01,0x45,0x3E,0x51,0x45,0x3E,0x51,0x40,0x00,
/*25*/ 0x00,0x87,0x2A,0xA9,0x83,0x0A,0xA9,0xC2,0x00,
/*26*/ 0x01,0x2A,0x94,0x10,0x82,0x14,0x6A,0x48,0x00,
/*27*/ 0x01,0x89,0x24,0x61,0x0A,0xA4,0x91,0xA0,0x00,
/*28*/ 0x00,0xC1,0x08,0x00,0x00,0x00,0x00,0x00,0x00,
/*29*/ 0x00,0x21,0x04,0x20,0x82,0x08,0x10,0x40,0x80,
/*2A*/ 0x02,0x04,0x10,0x20,0x82,0x08,0x41,0x08,0x00,
/*2B*/ 0x00,0x00,0x08,0xA9,0xCA,0x88,0x00,0x00,0x00,
/*2C*/ 0x00,0x00,0x08,0x23,0xE2,0x08,0x00,0x00,0x00,
/*2D*/ 0x00,0x00,0x00,0x00,0x00,0x00,0x20,0x84,0x00,
/*2E*/ 0x00,0x00,0x00,0x03,0xE0,0x00,0x00,0x00,0x00,
/*2F*/ 0x00,0x00,0x00,0x00,0x00,0x00,0x20,0x00,0x00,
/*30*/ 0x00,0x20,0x84,0x10,0x82,0x10,0x42,0x08,0x00,
/*31*/ 0x00,0xC4,0x92,0x49,0x24,0x92,0x48,0xC0,0x00,
/*32*/ 0x00,0x8E,0x08,0x20,0x82,0x08,0x20,0x80,0x00,
/*33*/ 0x01,0xC8,0xA2,0x08,0x42,0x10,0x83,0xE0,0x00,
/*34*/ 0x01,0xC8,0x82,0x30,0x20,0xA2,0x89,0xC0,0x00,
/*35*/ 0x00,0x43,0x0C,0x51,0x49,0x3E,0x10,0x40,0x00,
/*36*/ 0x03,0xC8,0x20,0xF2,0x20,0x82,0x89,0xC0,0x00,
/*37*/ 0x00,0xC4,0x20,0xF2,0x28,0xA2,0x89,0xC0,0x00,
/*38*/ 0x03,0xE0,0x84,0x10,0x42,0x08,0x20,0x80,0x00,
/*39*/ 0x01,0xC8,0xA2,0x72,0x28,0xA2,0x89,0xC0,0x00,
/*3A*/ 0x01,0xC8,0xA2,0x89,0xE0,0x82,0x11,0x80,0x00,
/*3B*/ 0x00,0x00,0x00,0x20,0x00,0x08,0x00,0x00,0x00,
/*3C*/ 0x00,0x00,0x08,0x00,0x02,0x08,0x40,0x00,0x00,
/*3D*/ 0x00,0x00,0x84,0x21,0x02,0x04,0x08,0x00,0x00,
/*3E*/ 0x00,0x00,0x00,0xF8,0x0F,0x80,0x00,0x00,0x00,
/*3F*/ 0x00,0x04,0x08,0x10,0x21,0x08,0x40,0x00,0x00,
/*40*/ 0x01,0xC8,0xA2,0x10,0x42,0x08,0x00,0x80,0x00,
/*41*/ 0x01,0xC8,0xA6,0xAA,0xAA,0xA4,0x89,0xC0,0x00,
/*42*/ 0x00,0x82,0x14,0x51,0x4F,0xA2,0x8A,0x20,0x00,
/*43*/ 0x03,0xC8,0xA2,0x93,0xC8,0xA2,0x8B,0xC0,0x00,
/*44*/ 0x00,0xC4,0xA2,0x82,0x08,0x22,0x48,0xC0,0x00,
/*45*/ 0x03,0x89,0x22,0x8A,0x28,0xA2,0x93,0x80,0x00,
/*46*/ 0x03,0xE8,0x20,0x83,0xC8,0x20,0x83,0xE0,0x00,
/*47*/ 0x03,0xE8,0x20,0x83,0xC8,0x20,0x82,0x00,0x00,
/*48*/ 0x00,0xC4,0xA0,0x82,0xE8,0xA2,0x48,0xE0,0x00,
/*49*/ 0x02,0x28,0xA2,0x8B,0xE8,0xA2,0x8A,0x20,0x00,
/*4A*/ 0x01,0xC2,0x08,0x20,0x82,0x08,0x21,0xC0,0x00,
/*4B*/ 0x00,0x41,0x04,0x10,0x41,0x24,0x91,0x80,0x00,
/*4C*/ 0x02,0x29,0x28,0xC2,0x8A,0x24,0x92,0x20,0x00,
/*4D*/ 0x02,0x08,0x20,0x82,0x08,0x20,0x83,0xE0,0x00,
/*4E*/ 0x02,0x28,0xB6,0xDB,0x6A,0xAA,0xAA,0x20,0x00,
/*4F*/ 0x02,0x2C,0xB2,0xAA,0xA9,0xA6,0x8A,0x20,0x00,
/*50*/ 0x01,0xC8,0xA2,0x8A,0x28,0xA2,0x89,0xC0,0x00,
/*51*/ 0x03,0xC8,0xA2,0x8B,0xC8,0x20,0x82,0x00,0x00,
/*52*/ 0x01,0xC8,0xA2,0x8A,0x28,0xAA,0x91,0xA0,0x00,
/*53*/ 0x03,0xC8,0xA2,0x8B,0xC9,0x22,0x8A,0x20,0x00,
/*54*/ 0x01,0xC8,0xA0,0x60,0x40,0xA2,0x89,0xC0,0x00,
/*55*/ 0x03,0xE2,0x08,0x20,0x82,0x08,0x20,0x80,0x00,
/*56*/ 0x02,0x28,0xA2,0x8A,0x28,0xA2,0x89,0xC0,0x00,
/*57*/ 0x02,0x28,0xA2,0x89,0x45,0x14,0x20,0x80,0x00,
/*58*/ 0x02,0xAA,0xAA,0xAA,0xAA,0x94,0x51,0x40,0x00,
/*59*/ 0x02,0x28,0x94,0x50,0x85,0x14,0x8A,0x20,0x00,
/*5A*/ 0x02,0x28,0x94,0x50,0x82,0x08,0x20,0x80,0x00,
/*5B*/ 0x03,0xE0,0x84,0x10,0x84,0x10,0x83,0xE0,0x00,
/*5C*/ 0x00,0xE2,0x08,0x20,0x82,0x08,0x20,0x82,0x0E,
/*5D*/ 0x02,0x28,0x94,0xF8,0x8F,0x88,0x20,0x80,0x00,
/*5E*/ 0x01,0xC1,0x04,0x10,0x41,0x04,0x10,0x41,0x1C,
/*5F*/ 0x00,0x85,0x22,0x00,0x00,0x00,0x00,0x00,0x00,
/*60*/ 0x00,0x00,0x00,0x00,0x00,0x00,0x03,0xE0,0x00,
/*61*/ 0x00,0xC2,0x04,0x00,0x00,0x00,0x00,0x00,0x00,
/*62*/ 0x00,0x00,0x1C,0x88,0x66,0xA2,0x99,0xA0,0x00,
/*63*/ 0x02,0x08,0x2C,0xCA,0x28,0xA2,0xCA,0xC0,0x00,
/*64*/ 0x00,0x00,0x1C,0x8A,0x08,0x20,0x89,0xC0,0x00,
/*65*/ 0x00,0x20,0x9A,0x9A,0x28,0xA2,0x99,0xA0,0x00,
/*66*/ 0x00,0x00,0x1C,0x8A,0x2F,0xA0,0x89,0xC0,0x00,
/*67*/ 0x00,0x62,0x1C,0x20,0x82,0x08,0x20,0x80,0x00,
/*68*/ 0x00,0x00,0x1E,0x8A,0x28,0xA6,0x68,0x28,0x9C,
/*69*/ 0x02,0x08,0x2C,0xCA,0x28,0xA2,0x8A,0x20,0x00,
/*6A*/ 0x00,0x80,0x08,0x20,0x82,0x08,0x20,0x80,0x00,
/*6B*/ 0x00,0x80,0x08,0x20,0x82,0x08,0x20,0x82,0x10,
/*6C*/ 0x02,0x08,0x20,0x92,0x8C,0x28,0x92,0x20,0x00,
/*6D*/ 0x00,0x82,0x08,0x20,0x82,0x08,0x20,0x80,0x00,
/*6E*/ 0x00,0x00,0x3C,0xAA,0xAA,0xAA,0xAA,0xA0,0x00,
/*6F*/ 0x00,0x00,0x2C,0xCA,0x28,0xA2,0x8A,0x20,0x00,
/*70*/ 0x00,0x00,0x1C,0x8A,0x28,0xA2,0x89,0xC0,0x00,
/*71*/ 0x00,0x00,0x2C,0xCA,0x28,0xA2,0xCA,0xC8,0x20,
/*72*/ 0x00,0x00,0x1A,0x9A,0x28,0xA2,0x99,0xA0,0x82,
/*73*/ 0x00,0x00,0x16,0x61,0x04,0x10,0x41,0x00,0x00,
/*74*/ 0x00,0x00,0x1C,0x89,0x03,0x02,0x89,0xC0,0x00,
/*75*/ 0x01,0x04,0x3C,0x41,0x04,0x10,0x41,0x80,0x00,
/*76*/ 0x00,0x00,0x22,0x8A,0x28,0xA2,0x99,0xA0,0x00,
/*77*/ 0x00,0x00,0x22,0x8A,0x25,0x14,0x20,0x80,0x00,
/*78*/ 0x00,0x00,0x22,0xAA,0xAA,0xAA,0xA9,0x40,0x00,
/*79*/ 0x00,0x00,0x22,0x51,0x42,0x14,0x52,0x20,0x00,
/*7A*/ 0x00,0x00,0x22,0x89,0x25,0x14,0x20,0x84,0x20,
/*7B*/ 0x00,0x00,0x3E,0x10,0x42,0x10,0x43,0xE0,0x00,
/*7C*/ 0x00,0x21,0x04,0x10,0x42,0x04,0x10,0x41,0x02,
/*7D*/ 0x00,0x82,0x08,0x20,0x82,0x08,0x20,0x82,0x08,
/*7E*/ 0x01,0x02,0x08,0x20,0x81,0x08,0x20,0x82,0x10,
/*7F*/ 0xFC,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
/*80*/ 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
/*A1*/ 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
/*A2*/ 0x00,0x00,0x00,0x00,0x00,0x00,0xE2,0x8E,0x00,
/*A3*/ 0x01,0xF4,0x10,0x41,0x00,0x00,0x00,0x00,0x00,
/*A4*/ 0x00,0x00,0x00,0x00,0x00,0x82,0x08,0x2F,0x80,
/*A5*/ 0x00,0x00,0x00,0x00,0x00,0x00,0x81,0x06,0x00,
/*A6*/ 0x00,0x00,0x00,0x00,0x03,0x0C,0x00,0x00,0x00,
/*A7*/ 0x00,0x0F,0x82,0x0B,0xE0,0x82,0x10,0x42,0x00,
/*A8*/ 0x00,0x00,0x00,0xF8,0x22,0x8C,0x20,0x84,0x00,
/*A9*/ 0x00,0x00,0x00,0x10,0x42,0x18,0xA0,0x82,0x00,
/*AA*/ 0x00,0x00,0x00,0x23,0xE8,0xA2,0x08,0x42,0x00,
/*AB*/ 0x00,0x00,0x00,0x03,0xE2,0x08,0x20,0x8F,0x80,
/*AC*/ 0x00,0x00,0x00,0x13,0xE3,0x14,0x52,0x41,0x00,
/*AD*/ 0x00,0x00,0x00,0x41,0x7E,0x52,0x20,0x82,0x00,
/*AE*/ 0x00,0x00,0x00,0x01,0xC1,0x04,0x10,0x4F,0xC0,
/*AF*/ 0x00,0x00,0x00,0x78,0x20,0x9E,0x08,0x27,0x80,
/*B0*/ 0x00,0x00,0x00,0xAA,0xAA,0x82,0x08,0x46,0x00,
/*B1*/ 0x00,0x00,0x00,0x03,0xE0,0x00,0x00,0x00,0x00,
/*B2*/ 0x00,0x0F,0x82,0x28,0xC3,0x08,0x41,0x08,0x00,
/*B3*/ 0x00,0x41,0x04,0x21,0x8A,0x08,0x20,0x82,0x00,
/*B4*/ 0x20,0x82,0x3E,0x8A,0x28,0x84,0x10,0x84,0x00,
/*B5*/ 0x00,0x0F,0x88,0x20,0x82,0x08,0x23,0xE0,0x00,
/*B6*/ 0x00,0x01,0x3E,0x10,0xC3,0x14,0x52,0x43,0x00,
/*B7*/ 0x20,0x82,0x3F,0x24,0x92,0x49,0x45,0x19,0x80,
/*B8*/ 0x00,0x83,0xB8,0x20,0xFF,0x04,0x10,0x41,0x00,
/*B9*/ 0x01,0x07,0x92,0x4A,0x20,0x84,0x10,0x8C,0x00,
/*BA*/ 0x01,0x04,0x1F,0x4A,0x20,0x82,0x10,0x84,0x00,
/*BB*/ 0x00,0x00,0x3E,0x08,0x20,0x82,0x0B,0xE0,0x80,
/*BC*/ 0x09,0x24,0xBF,0x49,0x20,0x82,0x10,0x42,0x00,
/*BD*/ 0x01,0x02,0x82,0x89,0x20,0x84,0x10,0x8C,0x00,
/*BE*/ 0x00,0x0F,0x82,0x08,0x41,0x0C,0x29,0x28,0x80,
/*BF*/ 0x01,0x04,0x16,0x6B,0x25,0x14,0x41,0x03,0x80,
/*C0*/ 0x00,0x08,0x92,0x49,0x20,0x84,0x10,0x8C,0x00,
/*C1*/ 0x00,0x83,0x8A,0x4B,0x23,0x84,0x10,0x8C,0x00,
/*C2*/ 0x00,0x67,0x04,0x13,0xF1,0x04,0x20,0x84,0x00,
/*C3*/ 0x00,0x8A,0xAA,0xAA,0x20,0x84,0x10,0x8C,0x00,
/*C4*/ 0x00,0x07,0x80,0x03,0xF1,0x04,0x10,0x84,0x00,
/*C5*/ 0x01,0x04,0x10,0x61,0x44,0x90,0x41,0x04,0x00,
/*C6*/ 0x00,0x41,0x3F,0x10,0x41,0x04,0x20,0x84,0x00,
/*C7*/ 0x00,0x00,0x1C,0x00,0x00,0x00,0x03,0xE0,0x00,
/*C8*/ 0x00,0x0F,0x82,0x48,0xC1,0x0A,0x29,0x08,0x00,
/*C9*/ 0x20,0x8F,0x82,0x10,0x87,0x2B,0x20,0x82,0x00,
/*CA*/ 0x00,0x00,0x82,0x08,0x21,0x04,0x23,0x00,0x00,
/*CB*/ 0x00,0x04,0x14,0x51,0x44,0x92,0x8A,0x28,0x80,
/*CC*/ 0x02,0x08,0x20,0x9B,0x88,0x20,0x82,0x07,0x80,
/*CD*/ 0x00,0x0F,0x82,0x08,0x20,0x82,0x10,0x8C,0x00,
/*CE*/ 0x00,0x02,0x14,0x4A,0x20,0x41,0x00,0x00,0x00,
/*CF*/ 0x20,0x82,0x3E,0x22,0xAA,0xAA,0xAA,0xA2,0x00,
/*D0*/ 0x00,0x0F,0x82,0x08,0x21,0x34,0x20,0x41,0x00,
/*D1*/ 0x01,0x81,0x80,0x40,0xC0,0x00,0xC0,0xC0,0x80,
/*D2*/ 0x00,0x82,0x08,0x41,0x45,0x22,0xBB,0x20,0x80,
/*D3*/ 0x00,0x20,0x82,0x50,0xC1,0x0A,0x29,0x08,0x00,
/*D4*/ 0x00,0x0F,0x10,0x43,0xE4,0x10,0x40,0xE0,0x00,
/*D5*/ 0x01,0x04,0x13,0x7B,0x25,0x08,0x20,0x82,0x00,
/*D6*/ 0x00,0x00,0x1C,0x10,0x41,0x04,0xFC,0x00,0x00,
/*D7*/ 0x00,0x0F,0x82,0x0B,0xE0,0x82,0x0B,0xE0,0x80,
/*D8*/ 0x01,0xE0,0x00,0xF8,0x20,0x82,0x08,0x46,0x00,
/*D9*/ 0x01,0x24,0x92,0x49,0x24,0x84,0x10,0x84,0x00,
/*DA*/ 0x00,0x45,0x14,0x51,0x45,0x56,0x5A,0x48,0x00,
/*DB*/ 0x01,0x04,0x10,0x41,0x04,0x92,0x51,0x84,0x00,
/*DC*/ 0x00,0x0F,0xA2,0x8A,0x28,0xA2,0x8B,0xE8,0x80,
/*DD*/ 0x00,0x0F,0xA2,0x8A,0x20,0x82,0x08,0x46,0x00,
/*DE*/ 0x00,0x08,0x92,0x08,0x20,0x82,0x10,0x8C,0x00,
/*DF*/ 0x02,0x8A,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
/*E0*/ 0x01,0x0A,0x10,0x00,0x00,0x00,0x00,0x00,0x00,
};

static u32 (*rgb2color)(u32 r, u32 g, u32 b);

void color_init()
{
	bgctx.bg_r=(config.bg_color>>16)&0xff,bgctx.bg_g=(config.bg_color>>8)&0xff,bgctx.bg_b=config.bg_color&0xff;
	bgctx.bg_a=(config.bg_color>>24);
	if(bgctx.bg_a==0) bgctx.bg_a=0xb0;
	
	u32 font_r=(config.font_color>>16)&0xff,font_g=(config.font_color>>8)&0xff,font_b=config.font_color&0xff;
	
	bgctx.rgb = rgb2color(bgctx.bg_r,bgctx.bg_g,bgctx.bg_b);
	bgctx.font_rgb = rgb2color(font_r,font_g,font_b);
}


static int font_output_32bit(int sx, int sy, const char *msg, int count)
{
	unsigned int * vram32 = (unsigned int *)bgctx.vram;

	int p, x, y, ox = sx;int offset;
	u8 pmap;u8 code;
#ifdef ENABLE_CLANG
	u8 * hz;u8 fontct;
#endif
	for(x = 0; x < count && msg[x]; x++)
	{
		fontct=0;
#ifdef ENABLE_CLANG
		if(IsHzcode(x,msg))
		{
			hz = GetHz(x,msg);
			if((((unsigned char)msg[x] +0x71)&0xFF)<0x6){
			x++;}
			pmap = *hz ++;
			for(y = 0; y < 12; y ++)
			{
				offset = (sy + y - 2) * bgctx.bufferwidth + ox;
				for(p = 0; p < 12; p ++)
				{
					if(pmap & 0x80)
						vram32[offset] = bgctx.font_rgb;
					pmap <<= 1;
					offset ++;
					fontct++;
					if((fontct&7)==0){
					pmap = *hz ++;
					}
				}
			}
			ox += 12;
			x ++;
		}
		else
		{
#endif

		code = (msg[x]-0x20);
		
	if(code<0xE0){
		//EUC半角
		if(code == 0x6E){//0x8E-0x20
		code= (msg[x+1]-0x40);
		x++;
		}

		pmap = efont[code*9];
		for(y = 0; y < 12; y ++)
		{
			offset = (sy + y) * bgctx.bufferwidth + ox;
			//pmap = efont[code * 8 + y];
			for(p = 0; p < 6; p ++)
			{
				if(pmap & 0x80)
					vram32[offset] = bgctx.font_rgb;
				pmap <<= 1;
				offset ++;
				fontct++;
				if((fontct&7)==0){
					pmap =efont[code*9+(fontct>>3)];
				}
			}
		}
		ox += 6;
	}
#ifdef ENABLE_CLANG
		}
#endif
	}
	return x;
}

static int font_output_16bit(int sx, int sy, const char *msg, int count)
{
	unsigned short * vram16 = (unsigned short *)bgctx.vram;

	int p, x, y, ox = sx;int offset;
	u8 pmap;u8 code;
#ifdef ENABLE_CLANG
	u8 * hz;u8 fontct;
#endif
	for(x = 0; x < count && msg[x]; x++)
	{
		fontct=0;
#ifdef ENABLE_CLANG
		if(IsHzcode(x,msg))
		{
			hz = GetHz(x,msg);
			if((((unsigned char)msg[x] +0x71)&0xFF)<0x6){
			x++;}
			pmap = *hz ++;
			for(y = 0; y < 12; y ++)
			{
				offset = (sy + y - 2) * bgctx.bufferwidth + ox;
				for(p = 0; p < 12; p ++)
				{
					if(pmap & 0x80)
						vram16[offset] = bgctx.font_rgb;
					pmap <<= 1;
					offset ++;
					fontct++;
					if((fontct&7)==0){
					pmap = *hz ++;
					}
				}
			}
			ox += 12;
			x ++;
		}
		else
		{
#endif
		code = (msg[x]-0x20);

	if(code<0xE0){
		//EUC半角
		if(code == 0x6E){//0x8E-0x20
		code= (msg[x+1]-0x40);
		x++;
		}

		pmap =  efont[code*9];
		for(y = 0; y < 12; y ++)
		{
			offset = (sy + y) * bgctx.bufferwidth + ox;
			//pmap = efont[code * 8 + y];
			for(p = 0; p < 6; p ++)
			{
				if(pmap & 0x80)
					vram16[offset] = bgctx.font_rgb;
				pmap <<= 1;
				offset ++;
				fontct++;
				if((fontct&7)==0){
					pmap =efont[code*9+(fontct>>3)];
				}
			}
		}
		ox += 6;
	}
#ifdef ENABLE_CLANG
		}
#endif
	}
	return x;
}

static void font_fillrect_32bit(int x1, int y1, int x2, int y2)
{
	unsigned int * vram32 = (unsigned int *)bgctx.vram;
	unsigned int * bufvram32 = (unsigned int *)BGbuf_get();
	int x, y;

	for(y = y1; y <= y2; y ++)
	{
		int offset = y * bgctx.bufferwidth + x1;
		int tmp = (y - BACKUP_Y1) * (BACKUP_X2-BACKUP_X1) - BACKUP_X1;
		for(x = x1; x <= x2; x ++)
		{
			if(x<BACKUP_X1 || x>=BACKUP_X2 || y<BACKUP_Y1 || y>=BACKUP_Y2 || bufvram32==NULL)
				vram32[offset] = bgctx.rgb;
			else vram32[offset] = bufvram32[x+tmp];
			offset ++;
		}
	}
}

static void font_fillrect_16bit(int x1, int y1, int x2, int y2)
{
	unsigned short * vram16 = (unsigned short *)bgctx.vram;
	unsigned short * bufvram16 = (unsigned short *)BGbuf_get();	
	int x, y;
	for(y = y1; y <= y2; y ++)
	{
		int offset = y * bgctx.bufferwidth + x1;
		int tmp = (y - BACKUP_Y1) * (BACKUP_X2-BACKUP_X1) - BACKUP_X1;
		for(x = x1; x <= x2; x ++)
		{
			if(x<BACKUP_X1 || x>=BACKUP_X2 || y<BACKUP_Y1 || y>=BACKUP_Y2 || bufvram16==NULL)
				vram16[offset] = bgctx.rgb;
			else vram16[offset] = bufvram16[x+tmp];
			offset ++;
		}
	}
}

static void font_line_32bit(int x1, int y1, int x2, int y2)
{
	unsigned int * vram32 = (unsigned int *)bgctx.vram;
	int dx = x2 - x1, dy = y2 - y1;
	if(dx < 0)
		dx = -dx;
	if(dy < 0)
		dy = -dy;
	int d = -dx, x = x1, y = y1;
	if(dx >= dy)
	{
		if(y2 < y1)
		{
			int t = x1; x1 = x2; x2 = t; t = y1; y1 = y2; y2 = t;
		}
		vram32 += y1 * bgctx.bufferwidth + x1;
		if(x1 < x2)
		{
			for(x = x1; x <= x2; x ++)
			{
				if(d > 0)
				{
					y ++;
					vram32 += 512;
					d -= 2 * dx;
				}
				d += 2 * dy;
				* vram32 = bgctx.font_rgb;
				vram32 ++;
			}
		}
		else
		{
			for(x = x1; x >= x2; x --)
			{
				if(d > 0)
				{
					y ++;
					vram32 += 512;
					d -= 2 * dx;
				}
				d += 2 * dy;
				* vram32 = bgctx.font_rgb;
				vram32 --;
			}
		}
	}
	else
	{
		if(x2 < x1)
		{
			int t = x1; x1 = x2; x2 = t; t = y1; y1 = y2; y2 = t;
		}
		vram32 += y1 * bgctx.bufferwidth + x1;
		if(y1 < y2)
		{
			for(y = y1; y <= y2; y ++)
			{
				if(d > 0)
				{
					x ++;
					vram32 ++;
					d -= 2 * dy;
				}
				d += 2 * dx;
				* vram32 = bgctx.font_rgb;
				vram32 += 512;
			}
		}
		else
		{
			for(y = y1; y >= y2; y --)
			{
				if(d > 0)
				{
					x ++;
					vram32 ++;
					d -= 2 * dy;
				}
				d += 2 * dx;
				* vram32 = bgctx.font_rgb;
				vram32 -= 512;
			}
		}
	}
}

static void font_line_16bit(int x1, int y1, int x2, int y2)
{
	unsigned short * vram16 = (unsigned short *)bgctx.vram;
	int dx = x2 - x1, dy = y2 - y1;
	if(dx < 0)
		dx = -dx;
	if(dy < 0)
		dy = -dy;
	int d = -dx, x = x1, y = y1;
	if(dx >= dy)
	{
		if(y2 < y1)
		{
			int t = x1; x1 = x2; x2 = t; t = y1; y1 = y2; y2 = t;
		}
		vram16 += y1 * bgctx.bufferwidth + x1;
		if(x1 < x2)
		{
			for(x = x1; x <= x2; x ++)
			{
				if(d > 0)
				{
					y ++;
					vram16 += 512;
					d -= 2 * dx;
				}
				d += 2 * dy;
				* vram16 = bgctx.font_rgb;
				vram16 ++;
			}
		}
		else
		{
			for(x = x1; x >= x2; x --)
			{
				if(d > 0)
				{
					y ++;
					vram16 += 512;
					d -= 2 * dx;
				}
				d += 2 * dy;
				* vram16 = bgctx.font_rgb;
				vram16 --;
			}
		}
	}
	else
	{
		if(x2 < x1)
		{
			int t = x1; x1 = x2; x2 = t; t = y1; y1 = y2; y2 = t;
		}
		vram16 += y1 * bgctx.bufferwidth + x1;
		if(y1 < y2)
		{
			for(y = y1; y <= y2; y ++)
			{
				if(d > 0)
				{
					x ++;
					vram16 ++;
					d -= 2 * dy;
				}
				d += 2 * dx;
				* vram16 = bgctx.font_rgb;
				vram16 += 512;
			}
		}
		else
		{
			for(y = y1; y >= y2; y --)
			{
				if(d > 0)
				{
					x ++;
					vram16 ++;
					d -= 2 * dy;
				}
				d += 2 * dx;
				* vram16 = bgctx.font_rgb;
				vram16 -= 512;
			}
		}
	}
}
/* 
static void* g_vram1;
static void* g_vram2;
static void* g_base;
 */
typedef struct _vram_ptr{
	void* g_vram1;
	void* g_vram2;
	void* g_base;
}t_vram_gv;

static t_vram_gv vram_gv __attribute__(   (  aligned( 4 ), section( ".bss" )  )   );

extern void font_get_vram()		//資函framebuffer adr,載仔載羽薦
{
	int i,unk;
	i=0;			//蝶乂短褒産喝議
	sceDisplayGetFrameBuf(&vram_gv.g_vram1, &bgctx.bufferwidth, &bgctx.pixelformat, &unk);
	do{
		sceDisplayWaitVblankStart();
		sceDisplayGetFrameBuf(&vram_gv.g_vram2, &bgctx.bufferwidth, &bgctx.pixelformat, &unk);
		i++;
	}while((vram_gv.g_vram1==vram_gv.g_vram2) && i<20);
	vram_gv.g_vram1 = (void *)(((unsigned long)vram_gv.g_vram1) | 0x40000000);
	vram_gv.g_vram2 = (void *)(((unsigned long)vram_gv.g_vram2) | 0x40000000);	
}

extern int font_init()
{
	int pwidth, pheight, unk;
	sceDisplayGetMode(&unk, &pwidth, &pheight);
	sceDisplayGetFrameBuf(&vram_gv.g_base, &bgctx.bufferwidth, &bgctx.pixelformat, unk);
	if(bgctx.bufferwidth == 0)		return -1;
	vram_gv.g_base = (void *)(((unsigned long)vram_gv.g_base) | 0x40000000);
	bgctx.vram = vram_gv.g_base;
	
	switch(bgctx.pixelformat)
	{
	case PSP_DISPLAY_PIXEL_FORMAT_565:
		rgb2color = rgb2color565;
		font_outputn = font_output_16bit;
		font_fillrect = font_fillrect_16bit;
		font_line = font_line_16bit;
		break;
	case PSP_DISPLAY_PIXEL_FORMAT_5551:
		rgb2color = rgb2color5551;
		font_outputn = font_output_16bit;
		font_fillrect = font_fillrect_16bit;
		font_line = font_line_16bit;
		break;
	case PSP_DISPLAY_PIXEL_FORMAT_4444:
		rgb2color = rgb2color4444;
		font_outputn = font_output_16bit;
		font_fillrect = font_fillrect_16bit;
		font_line = font_line_16bit;
		break;
	case PSP_DISPLAY_PIXEL_FORMAT_8888:
		rgb2color = rgb2color8888;
		font_outputn = font_output_32bit;
		font_fillrect = font_fillrect_32bit;
		font_line = font_line_32bit;
		break;
	default:
		return -1;
	}
	
	color_init();
	initBG();

	return 0;
}

void font_switch_vram()
{
 	if (bgctx.vram == vram_gv.g_vram1) bgctx.vram = vram_gv.g_vram2;
	else bgctx.vram = vram_gv.g_vram1;
}

extern void font_switch_refresh()
{
	sceDisplayWaitVblankStart();
	sceDisplaySetFrameBuf((void *)((unsigned long)bgctx.vram & ~0x40000000), bgctx.bufferwidth, bgctx.pixelformat, 0);
	font_switch_vram();
}

extern void font_switch_back()
{
	bgctx.vram = vram_gv.g_base;
	font_refresh();
}

extern void font_refresh()
{
	sceDisplayWaitVblankStart();
	sceDisplaySetFrameBuf((void *)((unsigned long)bgctx.vram & ~0x40000000), bgctx.bufferwidth, bgctx.pixelformat, 0);
}


