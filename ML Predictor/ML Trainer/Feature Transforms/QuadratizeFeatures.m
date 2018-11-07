function [quad] =  QuadratizeFeatures(X)
  [m,n] = size(X);
  quad = zeros(m, n * (n+1));
  exX = [ones(m,1) X];
  for i=1:(n+1)
    quad(:,(i*n-n+1):(i*n))=diag(exX(:,i))*X;
  end  
end