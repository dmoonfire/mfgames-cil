<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:muc="http://mfgames.com/2008/mfgames-utility-configuration"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
		>
  <!--
      Setup

      This handles the output settings and arguments passed in from code.
  -->
  <xsl:output method="text"/>

  <!--
      Class-Level Properties
  -->

  <xsl:template match="muc:configuration">
/* This is a tool-generated file using MfGames.Utility and
mfgames-utility.exe. Any changes to this file could be removed at the
next run of the tool and should be performed to the original XML
instead of this one. */

using MfGames.Settings;
using MfGames.Utility;
using System;

namespace <xsl:value-of select="muc:namespace"/>
{
public class <xsl:value-of select="muc:class"/>
: Settings
{
    #region Constructors
    public <xsl:value-of select="muc:class"/>(ISettings settings)
    : base(settings)
    {
    }
    #endregion
<xsl:apply-templates select="muc:group"/>
<xsl:text>}
</xsl:text>
}
  </xsl:template>

  <!--
      Groups
  -->

  <xsl:template match="muc:group">
    <xsl:text>
    #region Configuration Group: </xsl:text>
    <xsl:value-of select="muc:name"/>
    <xsl:text>
</xsl:text>
    <xsl:apply-templates select="muc:setting|muc:constant"/>
    <xsl:text>    #endregion
</xsl:text>
  </xsl:template>

  <!--
      Individual Properties
  -->

  <xsl:template match="muc:setting">
    <xsl:text>
    public </xsl:text>
    <xsl:value-of select="muc:type"/>
    <xsl:text> </xsl:text>
    <xsl:value-of select="muc:name"/>
    <xsl:text>
    {
        get
        {
            if (Contains("</xsl:text>
     <xsl:value-of select="../muc:name"/>
     <xsl:text>", "</xsl:text>
     <xsl:value-of select="muc:name"/>
     <xsl:text>"))
            {
                return </xsl:text>

<xsl:call-template name="variable-to-variable">
  <xsl:with-param name="value">
    <xsl:text>this["</xsl:text>
    <xsl:value-of select="../muc:name"/>
    <xsl:text>", "</xsl:text>
    <xsl:value-of select="muc:name"/>
    <xsl:text>"]</xsl:text>
  </xsl:with-param>
</xsl:call-template>

<xsl:text>;
            }
            else
            {</xsl:text>
<xsl:if test="muc:default">
<xsl:text>
                return </xsl:text>
<xsl:call-template name="string-to-variable">
  <xsl:with-param name="value">
   <xsl:value-of select="muc:default"/>
  </xsl:with-param>
</xsl:call-template>

<xsl:text>;</xsl:text>
</xsl:if>
<xsl:if test="not(muc:default)">
<xsl:text>
                return null;</xsl:text>
</xsl:if><xsl:text>
            }
        }

        set
        {
            this["</xsl:text>
    <xsl:value-of select="../muc:name"/>
    <xsl:text>", "</xsl:text>
    <xsl:value-of select="muc:name"/>
    <xsl:text>"] = </xsl:text>
    <xsl:call-template name="value-to-string"/>
    <xsl:text>;
        }
    }
</xsl:text>
  </xsl:template>

  <xsl:template match="muc:constant">
    <xsl:text>
    public </xsl:text>
    <xsl:value-of select="muc:type"/>
    <xsl:text> </xsl:text>
    <xsl:value-of select="muc:name"/>
    <xsl:text>
    {
        get
        {
            return </xsl:text>
<xsl:call-template name="string-to-variable">
  <xsl:with-param name="value">
   <xsl:value-of select="muc:default"/>
  </xsl:with-param>
</xsl:call-template>

<xsl:text>;
        }
    }
</xsl:text>
  </xsl:template>

  <!--
      Variable mapping and translation.
  -->

  <xsl:template name="string-to-variable">
    <xsl:param name="value"/>

    <xsl:choose>
      <xsl:when test="muc:type = 'string'">
	<xsl:text>"</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>"</xsl:text>
      </xsl:when>
      <xsl:when test="muc:type = 'int' or
		      muc:type = 'long' or
		      muc:type = 'double' or
		      muc:type = 'float' or
		      muc:type = 'bool'">
	<xsl:value-of select="$value"/>
      </xsl:when>
      <xsl:when test="muc:type/@format = 'Enumeration'">
	<xsl:value-of select="$value"/>
      </xsl:when>
      <xsl:otherwise>
	<xsl:text>new </xsl:text>
	<xsl:value-of select="muc:type"/>
	<xsl:text>("</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>")</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="variable-to-variable">
    <xsl:param name="value"/>

    <xsl:choose>
      <xsl:when test="muc:type = 'string'">
	<xsl:value-of select="$value"/>
      </xsl:when>
      <xsl:when test="muc:type = 'bool'">
	<xsl:text>Boolean.Parse(</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>)</xsl:text>
      </xsl:when>
      <xsl:when test="muc:type = 'int'">
	<xsl:text>Int32.Parse(</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>)</xsl:text>
      </xsl:when>
      <xsl:when test="muc:type = 'long'">
	<xsl:text>Int64.Parse(</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>)</xsl:text>
      </xsl:when>
      <xsl:when test="muc:type = 'double'">
	<xsl:text>Double.Parse(</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>)</xsl:text>
      </xsl:when>
      <xsl:when test="muc:type = 'float'">
	<xsl:text>Float.Parse(</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>)</xsl:text>
      </xsl:when>
      <xsl:when test="muc:type/@format = 'Enumeration'">
	<xsl:text>(</xsl:text>
	<xsl:value-of select="muc:type"/>
	<xsl:text>) Enum&lt;</xsl:text>
	<xsl:value-of select="muc:type"/>
	<xsl:text>&gt;.Parse(</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>)</xsl:text>
      </xsl:when>
      <xsl:otherwise>
	<xsl:text>new </xsl:text>
	<xsl:value-of select="muc:type"/>
	<xsl:text>(</xsl:text>
	<xsl:value-of select="$value"/>
	<xsl:text>)</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="value-to-string">
    <xsl:choose>
      <xsl:when test="muc:type = 'string'">
	<xsl:text>value</xsl:text>
      </xsl:when>
      <xsl:when test="muc:type = 'System.IO.FileInfo'">
	<xsl:text>value.FullName</xsl:text>
      </xsl:when>
      <xsl:otherwise>
	<xsl:text>value.ToString()</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
