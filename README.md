# ImageResizer.Plugins.Log4net

## Description

Log4net logging for ImageResizer

## How to get started?

Requires ImageResizer 4.0.1 or above

- `install-package ImageResizer.Plugins.Log4net`

The following will be added to `web.config`

```
<configuration>
    <resizer>
        <plugins>
            <add name="Log4net" />
        </plugins>
    </resizer>    
</configuration>
```

## Additional configuration

For detailed settings an `<log4net>` element can be provided inside `<resizer>`

```
<configuration>
    <resizer>
        ...
        <log4net configFile="log4net.config" />
        <plugins>
            ...
            <add name="Log4net" />
        </plugins>
    </resizer>    
</configuration>
```

This element has a list of attributes that can be provided for detailed control over plugin behaviour.

| Parameter | Description |
| --------- | ----------- |
| configFile | Path to log4net configuration file (omit if log4net is configured via web.config or via assembly attribute) |

## Package maintainer

https://github.com/svenrog

## Changelog

[Changelog](CHANGELOG.md)