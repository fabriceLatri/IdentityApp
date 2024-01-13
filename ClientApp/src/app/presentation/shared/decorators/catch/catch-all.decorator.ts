// type HandlerFunction<T> = (error: Error, ctx: T) => void;

// export const Catch = <T>(errorType: any, handler: HandlerFunction<T>): any => {
//   return (target: T, propertyKey: keyof T, descriptor: PropertyDescriptor) => {
//     // Save a reference to the original method
//     const originalMethod = descriptor.value;

//     // Rewrite original method with try/catch wrapper
//     descriptor.value = function (...args: any[]) {
//       try {
//         const result = originalMethod.apply(this, args);

//         // Check if method is asynchronous
//         if (result && result instanceof Promise) {
//           // Return promise
//           return result.catch((error: any) => {
//             _handleError(target, errorType, handler, error);
//           });
//         }

//         // Return actual result
//         return result;
//       } catch (error) {
//         if (error instanceof Error)
//           _handleError(target, errorType, handler, error);
//       }
//     };

//     return descriptor;
//   };
// };

// export const CatchAll = <T>(handler: HandlerFunction<T>): any =>
//   Catch(Error, handler);

// function _handleError<T>(
//   ctx: T,
//   errorType: any,
//   handler: HandlerFunction<T>,
//   error: Error
// ) {
//   // Check if error is instance of given error type
//   if (typeof handler === 'function' && error instanceof errorType) {
//     // Run handler with error object and class context
//     handler(error, ctx);
//   } else {
//     // Throw error further
//     // Next decorator in chain can catch it
//     throw error;
//   }
// }

type HandlerFunction<T> = (error: Error, ctx: T) => void;

export const Catch = <T>(errorType: any, handler: HandlerFunction<T>): any => {
  return (target: any, propertyKey: string, descriptor: PropertyDescriptor) => {
    // Save a reference to the original method
    const originalMethod = descriptor.value;

    // Rewrite original method with try/catch wrapper
    descriptor.value = function (...args: any[]) {
      try {
        const result = originalMethod.apply(this, args);

        // Check if method is asynchronous
        if (result && result instanceof Promise) {
          // Return promise
          return result.catch((error: any) => {
            _handleError(this, errorType, handler, error);
          });
        }

        // Return actual result
        return result;
      } catch (error) {
        if (error instanceof Error)
          _handleError(this, errorType, handler, error);
      }
    };

    return descriptor;
  };
};

export const CatchAll = <T>(handler: HandlerFunction<T>): any =>
  Catch(Error, handler);

function _handleError<T>(
  ctx: any,
  errorType: any,
  handler: HandlerFunction<T>,
  error: Error
) {
  // Check if error is instance of given error type
  if (typeof handler === 'function' && error instanceof errorType) {
    // Run handler with error object and class context
    handler(error, ctx);
  } else {
    // Throw error further
    // Next decorator in chain can catch it
    throw error;
  }
}
