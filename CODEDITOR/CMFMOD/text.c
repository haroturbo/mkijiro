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

#include <pspkernel.h>
#include <pspsysmem_kernel.h>
#include <pspsuspend.h>
#include "layout.h"
#include "text.h"
#include "font.h"
#include "allocmem.h"
#include "encode.h"

extern int text_open(const char * filename, int rowbytes, p_txtpack txtpack)
{
	int fd = sceIoOpen(filename, PSP_O_RDONLY, 0777);
	if(fd < 0)
		return 1;
	int l = sceIoLseek32(fd, 0, PSP_SEEK_END);
	if(l<rowbytes) l=rowbytes;

	txtpack->txt = malloc(sizeof(t_text));
	if(txtpack->txt == NULL)
	{
		sceIoClose(fd);
		return 1;
	}
	
	txtpack->txt->buf = malloc(l+4);
	if(txtpack->txt->buf == NULL)
	{
		sfree(txtpack->txt);
		sceIoClose(fd);
		return 1;
	}
 
	txtpack->txt->buf[l] = 0;
	sceIoLseek32(fd, 0, PSP_SEEK_SET);
	sceIoRead(fd, txtpack->txt->buf, l);
	sceIoClose(fd);
	txtpack->txt->size = l;

#ifdef BIG5_ENCODE_TEXT
	t_encodepack pack;
	if(big5_init((char *)txtpack->txt->rows,&pack)==0 && encode_init(&pack)==0){
		charsets_big5_conv(txtpack->txt->buf,&pack);
		encode_free(&pack);
	}
#endif
	
 	char * pos = txtpack->txt->buf, * posend = pos + txtpack->txt->size;
	txtpack->txt->row_count = 0;
	while(txtpack->txt->row_count < MAX_TEXTROW_COUNT && pos + 1 < posend)
	{
		txtpack->txt->rows[txtpack->txt->row_count].start = pos;
		char * startp = pos, * endp = pos + rowbytes;
		if(endp > posend)
			endp = posend;
		while(pos < endp && *pos != 0 && *pos != '\r' && *pos != '\n')
			if((*(unsigned char *)pos) >= 0x80)
				pos += 2;
			else
				++ pos;
		if(pos > endp)
			pos -= 2;
		if(pos + 1 < posend && ((*pos >= 'A' && *pos <= 'Z') || (*pos >= 'a' && *pos <= 'z')))
		{
			char * curp = pos - 1;
			while(curp > startp)
			{
				if(*(unsigned char *)(curp - 1) >= 0x80 || *curp == ' ' || * curp == '\t')
				{
					pos = curp + 1;
					break;
				}
				curp --;
			}
		}
		txtpack->txt->rows[txtpack->txt->row_count].count = pos - startp;
		if(*pos == 0 || *pos == '\r' || *pos =='\n')
		{
			if(*pos == '\r' && *(pos + 1) == '\n')
				pos += 2;
			else
				++ pos;
		}
		txtpack->txt->row_count ++;
	}

	return 0;
}

extern int text_rows(p_txtpack txtpack)
{
/* 	if(txtpack->txt == NULL)
		return 0; */
	return txtpack->txt->row_count;
}

extern p_textrow text_read(int row, p_txtpack txtpack)
{
/* 	if(txtpack->txt == NULL || row >= txtpack->txt->row_count)
		return NULL; */
	return &txtpack->txt->rows[row];
}

extern void text_close(p_txtpack txtpack)
{
	sfree(txtpack->txt->buf);
	sfree(txtpack->txt);
}
